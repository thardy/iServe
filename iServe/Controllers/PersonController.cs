using System.Web.Mvc;
using iServe.Models;
using iServe.Models.F1APIServices;
using iServe.Models.dotNailsCommon;

namespace iServe.Controllers {
    // Add once login is implemented --> [Authorize]
    public class PersonController : iServeController {
        #region Private Fields
        private PersonAPI personAPI;
        #endregion

        public IModelFactory<iServeDBProcedures> Model { get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the iServe.Controllers.PersonController class.
        /// </summary>
		public PersonController() {
            this.personAPI = new PersonAPI();
            this.Model = new ModelFactory<iServeDBProcedures>(new iServeDBDataContext(), GetCurrentUserID());
		}

		public PersonController(IModelFactory<iServeDBProcedures> modelFactory) {
            this.personAPI = new PersonAPI();			
            this.Model = modelFactory;
        }

        /// <summary>
        /// Initializes a new instance of the iServe.Controllers.PersonController class.
        /// </summary>
        /// <param name="personAPI">
        /// A PersonAPI instance that can be used for testing (a mock object).
        /// </param>
        public PersonController(PersonAPI personAPI) {
            this.personAPI = personAPI;
        }

        public PersonController(PersonAPI personAPI, IModelFactory<iServeDBProcedures> modelFactory) {
            this.personAPI = personAPI;
            this.Model = modelFactory;
        }
        #endregion
              
        #region Action Methods
        /// <summary>
        /// Displays a read-only view of the user's profile information.
        /// </summary>
        /// <param name="id">
        /// The individual id of the user.
        /// </param>
        public ActionResult Show(int id) {
            int personID = id;
            
            Person person = this.personAPI.GetPerson(personID, AccessToken);
            
            return View(person);
        }

        /// <summary>
        /// Displays an editable view of the user's profile information.
        /// </summary>
        /// <param name="id">
        /// The individual id of the user.
        /// </param>
        public ActionResult Edit(int id) {
            int individualID = id;

            Person person = this.personAPI.GetPerson(individualID, AccessToken);
           
            return View(person);
        }

        /// <summary>
        /// Updates the user's profile information.
        /// </summary>
        /// <param name="id">
        /// The individual id of the user.
        /// </param>
        /// <param name="collection">
        /// A collection of form values used to update the user's profile.
        /// </param>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "ID, HouseholdID, FirstName, LastName, DateofBirth, IndividualEmailID, IndividualEmail, IndividualPhoneNumberID, IndividualPhoneNumber, HouseholdEmailID, HouseholdEmail, HouseholdPhoneNumberID, HouseholdPhoneNumber")]Person person, [Bind(Include="ID, Address1, Address2, City, StProvince, PostalCode")]Address address) {
            try {
                person.Address = address;
                if (ModelState.IsValid) {
                    this.personAPI.UpdatePerson(person, AccessToken);

                    // Get the user in order to update the email address stored in the User table.
                    User user = this.Model.New<User>().GetByKey(int.Parse(person.ID));

                    if (user != null && !string.IsNullOrEmpty(person.IndividualEmail)) {
                        // Update the user's email address in the User table.
                        // This is the address used to send messages.
                        user.Email = person.IndividualEmail;
                        user.Save();
                    }

                    return RedirectToAction("Show", new { id = person.ID });
                }
                else {
                    return View("Edit", person);
                }
            }
            catch {
                return View("Edit", person);
            }
        }
        #endregion
    }
}
