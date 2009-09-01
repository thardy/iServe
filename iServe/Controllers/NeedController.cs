using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iServe.Models;
using iServe.Models.dotNailsCommon;
//using xVal.ServerSide;
using iServe.Models.Security;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace iServe.Controllers {
	public class NeedController : iServeController, IDotNailsController {
		#region Properties
		public IModelFactory<iServeDBProcedures> Model { get; set; }
		
		public int CurrentPageNumber {
			get {
				int page = 1;

				if (this.Request.Params["page"] != null) {
					int.TryParse(this.Request.Params["page"], out page);
				}

				return page;
			}
		}
		#endregion Properties
		
		public NeedController() : base() {
			Model = new ModelFactory<iServeDBProcedures>(new iServeDBDataContext(), GetCurrentUserID());
		}
		public NeedController(IModelFactory<iServeDBProcedures> modelFactory, User currentUser) : base(currentUser) {
			Model = modelFactory;
        }

        #region Action Methods
        public ActionResult Index() {
			Model.DataContext.Log = new DebugTextWriter();
			GetNeedsResultList needs = null;
			int? rowCount = 0;

			if (Request.IsAuthenticated) {
				needs = Model.SPs.GetNeeds(CurrentUser.ChurchID, CurrentUser.ID, CurrentPageNumber, Config.RecordsPerPage, "Created", false, ref rowCount);
			}

			return View("Index", needs);
		}

		public ActionResult Show(int id) {
			Need need = Model.New<Need>().GetByKey(id);

			return View(need);
		}

		public ActionResult New() {
			Need need = Model.New<Need>();
			need.HelpersNeeded = 1;
			
			LoadSelectLists(need);
			
			return View(need);
		}

		public ActionResult Create(Need need) {
			if (ModelState.IsValid) {
				try {
					need.SubmittedByID = CurrentUser.ID;
					need.ChurchID = CurrentUser.ChurchID;
					need.NeedStatusID = (int)NeedStatusEnum.Pending;
					need.Save();

					return RedirectToAction("Index");
				}
				catch (Exception ex) {
					// put exception somewhere on screen or redirect
					throw (ex);
				}
			}
			
			// Unsuccessful, so load the necessary data for the form and show it again
			LoadSelectLists(need);
			return View("New", need);
		}

		public ActionResult Edit(int id) {
			return View();
		}

		// TODO: Implement this.  Currently unused and untested.
		public ActionResult Update(int id) {
			Need need = Model.New<Need>();
			need.ID = id;
			
			// Bind data from form
			
			// Save
			need.Save();

			return RedirectToAction("Index", "Need");
		}

		public ActionResult Dashboard() {
			return View();
		}

		public ActionResult iNeed() {
            // Create and run the query to return a list of needs that are submitted by the user.
            var needs = Model.New<Need>().GetByUserID(this.CurrentUser.ID);

            // Get the number of interested and committed users per need.
            List<int> unratedCommittedUsers = new List<int>();
            List<int> interestedUsers = new List<int>();
            foreach (var need in needs) {
                int committedUserCount = Model.New<UserNeed>().GetUnratedCommittedUsersCount(need.ID);
                int interestedUserCount = Model.New<UserNeed>().GetInterestedUsersCount(need.ID);

                if (committedUserCount > 0) {
                    unratedCommittedUsers.Add(need.ID);
                }

                if (interestedUserCount > 0) {
                    interestedUsers.Add(need.ID);
                }
            }

            // Add interested and committed user counts to ViewData in order to retrieve it from the page.
            ViewData["InterestedUsers"] = interestedUsers;
            ViewData["UnratedCommittedUsers"] = unratedCommittedUsers;

            // Only get the needs that are met but with committed users that haven't been rated or needs that haven't been cancelled or met.
            List<Need> openNeeds = needs.Where(need => ((NeedStatusEnum)need.NeedStatusID == NeedStatusEnum.Met && unratedCommittedUsers.Contains(need.ID)) || ((NeedStatusEnum)need.NeedStatusID != NeedStatusEnum.Cancelled) && (NeedStatusEnum)need.NeedStatusID != NeedStatusEnum.Met).ToList();

            // Return the partial view with the list of needs.
			return View("_iNeed", openNeeds);
		}

		public ActionResult iServe() {
            List<GetNeedInfoByHelperResult> needInfo = Model.SPs.GetNeedInfoByHelper(CurrentUser.ID, CurrentUser.ChurchID).ToList();

            //Dictionary<int, string> userNeedStatuses;
            //List<Need> needs = Model.New<Need>().GetAllByUser(CurrentUser.ID, out userNeedStatuses);
            //var unMetNeeds = needs.Where(need => need.NeedStatusID != (int)NeedStatusEnum.Met); 

            //ViewData["UserNeedStatuses"] = userNeedStatuses;

            //ViewData["CanRate"] = canRate;
            
            // Return the partial view with the list of needs.
            return View("_iServe", needInfo);
		}

		public ActionResult NeedAdmin() {
			List<Need> needs = Model.New<Need>().GetAll().ToList();
			return View("NeedAdmin", needs);
        }
        #endregion

        #region Ajax methods
		public ActionResult ExpressInterest(int id) {
            try {
                UserNeed userNeed = Model.New<UserNeed>();
                userNeed.EntityState = EntityStateEnum.Added;
                userNeed.UserID = CurrentUser.ID;
                userNeed.NeedID = id;
                userNeed.UserNeedStatusID = (int)UserNeedStatusEnum.Interested;
                userNeed.Save();

                // Send message to submitter.
                //SendMessageToSubmitter(id);
            }
            catch (Exception ex) {
				ViewData["error"] = ex.Message + "<br/> Inner Exception: <br/>";
				if (ex.InnerException != null)
					ViewData["error"] += ex.InnerException.Message;

                return View("Error");
            }

			//int? rowCount = 0;
			//GetNeedsResultList needs = Model.SPs.GetNeeds(CurrentUser.ChurchID, CurrentUser.ID, CurrentPageNumber, RecordsPerPage, "Created", false, ref rowCount);
			//return View("Index", needs);

			//return new RedirectResult(Url.Action("Index"));

            return new EmptyResult();
		}

		/// <summary>
        /// Shows the users that have expressed interest or have committed to the specified need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need.
        /// </param>
        /// <returns></returns>
        public ActionResult ShowInvolvement(int needID) {
            // Get the need with the specified id that is submitted by the current user.
            Need need = Model.New<Need>().GetByKeyAndUserID(needID, this.CurrentUser.ID);

            if (need != null) {
                // Get committed users.
				List<GetNeedHelpersByStatusResult> committedUsers = Model.SPs.GetNeedHelpersByStatus(needID, (int)UserNeedStatusEnum.Committed, CurrentUser.ChurchID).ToList();
                ViewData["CommittedUsers"] = committedUsers;

                // Get accepted users.
				List<GetNeedHelpersByStatusResult> acceptedUsers = Model.SPs.GetNeedHelpersByStatus(needID, (int)UserNeedStatusEnum.Accepted, CurrentUser.ChurchID).ToList();
				ViewData["AcceptedUsers"] = acceptedUsers;

                // Get interested users.
				List<GetNeedHelpersByStatusResult> interestedUsers = Model.SPs.GetNeedHelpersByStatus(needID, (int)UserNeedStatusEnum.Interested, CurrentUser.ChurchID).ToList();
                ViewData["InterestedUsers"] = interestedUsers;

                return View("_needInvolvement", need);
            }

            ViewData["error"] = "The need doesn't exist or you do not have permissions to access this need.";

            return View("Error");
        }

        /// <summary>
        /// Accepts a user that has expressed interest in a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need that the user has expressed interest in.
        /// </param>
        /// <param name="userID">
        /// The id of the user to accept.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AcceptUser(int needID, int userID) {
			UserNeed userNeed = Model.New<UserNeed>();
			userNeed.EntityState = EntityStateEnum.Modified;
			userNeed.UserID = userID;
			userNeed.NeedID = needID;
			userNeed.UserNeedStatusID = (int)UserNeedStatusEnum.Accepted;
			userNeed.Save(); 

            return new EmptyResult();            
        }

        /// <summary>
        /// Declines a user that has expressed interest in a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need that the user has expressed interest in.
        /// </param>
        /// <param name="userID">
        /// The id of the user to decline.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeclineUser(int needID, int userID) {
            UserNeed userNeed = Model.New<UserNeed>();
            userNeed.EntityState = EntityStateEnum.Modified;
            userNeed.UserID = userID;
            userNeed.NeedID = needID;
            userNeed.UserNeedStatusID = (int)UserNeedStatusEnum.SubmitterDeclined;
            userNeed.Save(); 
            
            return new EmptyResult();
        }

        /// <summary>
        /// Removes a user's involvement in a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need that the user is involved with.
        /// </param>
        /// <param name="userID">
        /// The id of the user.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveUser(int needID, int userID) {
            UserNeed userNeed = Model.New<UserNeed>();
            userNeed.EntityState = EntityStateEnum.Deleted;
            userNeed.UserID = userID;
            userNeed.NeedID = needID;
            userNeed.Save(); 

            return new EmptyResult();
        }

        /// <summary>
        /// Commits a user to a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need to commit the user to.
        /// </param>
        /// <param name="userID">
        /// The id of the user.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommitUser(int needID, int userID) {
            UserNeed userNeed = Model.New<UserNeed>();
            userNeed.EntityState = EntityStateEnum.Modified;
            userNeed.UserID = userID;
            userNeed.NeedID = needID;
            userNeed.UserNeedStatusID = (int)UserNeedStatusEnum.Committed;
            userNeed.Save(); 
            
            return new EmptyResult();
        }

        /// <summary>
        /// Rates a committed helper after the need has been met or cancelled.
        /// </summary>
        /// <param name="rating">
        /// The rating value to add to the helper's total rating.
        /// </param>
        /// <param name="userID">
        /// The user id of the helper.
        /// </param>
        /// <param name="needID">
        /// The id of the need.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RateHelper(int rating, int userID, int needID) {
            // Get the helper to obtain the helper's current rating.
            User user = Model.New<User>().GetByKey(userID);
            
            // Update the helper's rating.
            user.Rating += rating;
            user.Save();

            // Remember that the helper has been rated so that the submitter cannot rate the helper again.
            UserNeed helper = Model.New<UserNeed>().GetByKey(userID, needID);
            helper.HasBeenRated = true;
            helper.Save();
            
            return new EmptyResult();
        }

        /// <summary>
        /// Rates the submitter after the need has been met or cancelled.
        /// </summary>
        /// <param name="rating">
        /// The rating value to add to the submitter's total rating.
        /// </param>
        /// <param name="userID">
        /// The user id of the submitter.
        /// </param>
        /// <param name="needID">
        /// The id of the need.
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RateSubmitter(int rating, int userID, int needID) {
            // Get the submitter to obtain the submitter's current rating.
            User user = Model.New<User>().GetByKey(userID);

            // Update the helper's rating.
            user.Rating += rating;
            user.Save();

            // Remember that the submitter has been rated so that the helper cannot rate the submitter again.
            UserNeed helper = Model.New<UserNeed>().GetByKey(this.CurrentUser.ID, needID);
            helper.HasRatedSubmitter = true;
            helper.Save();

            return new EmptyResult();
        }

        /// <summary>
        /// Completes a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need to complete
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompleteNeed(int needID) {
            Model.SPs.CompleteNeed(needID);

            return new EmptyResult();
        }

        /// <summary>
        /// Cancels a need.
        /// </summary>
        /// <param name="needID">
        /// The id of the need to cancel
        /// </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CancelNeed(int needID) {
			Model.SPs.CancelNeed(needID);
			
	        return View("Dashboard");
        }
        #endregion

        #region Private Methods
        /// <summary>
		/// Creates SelectLists for Foreign-keys
		/// </summary>
		private void LoadSelectLists(Need need) {
			var categories = Model.New<Category>().GetAll().ToList();
			ViewData["Categories"] = new SelectList(categories, "ID", "Name", need.CategoryID);
		}

        /// <summary>
        /// Sends an email 
        /// </summary>
        /// <param name="userID">
        /// The id of the helper to send the message to. 
        /// If the id is null, the message will be sent to the submitter of the need.
        /// </param>
        public ActionResult SendMessage(int needID, int? userID, string messageText) {
            try {
				string fromEmail = Config.FromEmail;
                string smtpServer = Config.SMTPServer;

                // Get the need (we use the name)
                Need need = Model.New<Need>().GetByKey(needID);

                // Get the user to send to.
                User user = null;
                if (userID != null && userID != int.MinValue) {
                    user = Model.New<User>().GetByKey(userID.Value);
                }
                else {
                    user = Model.New<User>().GetSubmitterByNeedID(needID);
                }

                string subject = string.Format("You received a message from {0} regarding need: {1}", CurrentUser.Name, need.Name);
                StringBuilder body = new StringBuilder();
                body.AppendFormat("<b>Username:</b> {0}<br />", CurrentUser.Name);
                body.AppendFormat("<b>Need:</b> {0}<br />", need.Name);
                body.AppendFormat("<b>Message:</b><br />{0}", messageText);

                // Save the message
                Message message = Model.New<Message>();
                message.Body = body.ToString();
                message.NeedID = needID;
                message.FromUserID = CurrentUser.ID;
                message.ChurchID = CurrentUser.ChurchID;
                message.Save();

                // Send the message as an email to the user
                MailMessage mailMessage = new MailMessage(fromEmail, user.Email, subject, body.ToString());
                mailMessage.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(Config.SMTPServer);
				NetworkCredential credentials = new NetworkCredential(Config.SMTPUsername, Config.SMTPPassword);
				client.UseDefaultCredentials = false;
				client.Credentials = credentials;
				
                client.Send(mailMessage);
            }
            catch (Exception ex) {
                ViewData["error"] = ex.Message + "<br/> Inner Exception: <br/>";
                if (ex.InnerException != null)
                    ViewData["error"] += ex.InnerException.Message;

                return View("Error");
            }

            return new EmptyResult();
        }

		#endregion Private Methods

		#region IdotNailsController Members

		public IEntity CreateModelEntityUsingDefaultFactory(Type type) {
			return Model.New(type);
		}

		#endregion
	}

	
}
