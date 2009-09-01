$(document).ready(function() {
    Message.InitializeDialog();
});

Dashboard = {
    RateUserUrl: '',
    RateUserItemID: '',

    Loaded: function() {
        // Load tabs.
        $('#tabs').tabs({ fx: { height: 'toggle', opacity: 'toggle'} });

        // Initialize modal dialog for rating users.
        $('#rate_user_dialog').dialog({
            bgiframe: true,
            autoOpen: false,
            width: 300,
            modal: true,
            buttons: {
                'Negative': function() {
                    $(this).dialog('close');
                    Dashboard.RateUser(-1, Dashboard.RateUserItemID, Dashboard.RateUserUrl);
                },
                'Neutral': function() {
                    $(this).dialog('close');
                    Dashboard.RateUser(0, Dashboard.RateUserItemID, Dashboard.RateUserUrl);
                },
                'Positive': function() {
                    $(this).dialog('close');
                    Dashboard.RateUser(1, Dashboard.RateUserItemID, Dashboard.RateUserUrl);
                },
                Cancel: function() {
                    $(this).dialog('close');
                }
            }
        });
    },

    ShowiNeed: function(url) {
        // Get the iNeed content to render to the Dashboard page.
        $.get(url, function(data) {
            // Update the iNeed left section with the new content and show it on the page.
            $('#ineed_left_section').html(data);

            // Clear the iNeed involvement section.
            $('#ineed_right_section').html('');
        });
    },

    ShowiServe: function(url) {
        // Get the iServe content to render to the Dashboard page.
        $.get(url, function(data) {
            // Update the iServe left section with the new content and show it on the page.
            $('#iserve_left_section').html(data);
        });
    },



    //
    // Shows the involvement section pertaining to the selected need.
    //
    ShowInvolvement: function(itemID, url) {
        // Get the involvement content for the need that the user clicked on.
        $.get(url, function(data) {
            // Update the involvement section with the new content and show it on the page.
            $('#ineed_right_section').html(data); //.fadeIn('fast', function() {
            // Change the vertical positioning of the involvement section to align 
            // with the need list item.

            // Ensure the involvement section stays within the bounds of the need list.

            var firstItemTop = $('.need_list li:first').offset().top;
            var selectedItemTop = $('#' + itemID).offset().top;

            var listItems = $('#ineed_left_section .need_list').children();
            var lastItemTop = listItems.eq(listItems.length - 1).offset().top;
            var lastItemBottom = lastItemTop + listItems.eq(listItems.length - 1).height();

            var boxTop = $('#ineed_left_section').offset().top;
            var boxBottom = boxTop + $('#ineed_left_section').height();

            var top = boxBottom - $('#ineed_right_section').height();

            var newTop = selectedItemTop;
            var newBottom = newTop + $('#ineed_right_section').height();
            if (boxBottom - newBottom < 0) {
                newTop = top;
            }

            $('#ineed_right_section').animate({ 'top': newTop - firstItemTop });
        });
    },

    //
    // Modifies the user's involvement status with the need (Accept, Decline, Cancel, etc).
    //
    ModifyUserInvolvementStatus: function(needID, userID, elementID, url) {
        // Display modal dialog for sending a message to the helper.
        Message.ShowNewMessageDialog(needID, userID, elementID);

        // Call the ajax method to modify the user's involvement status.
        $.post(url, function() {
            
        });
    },

    //
    // Completes a need
    //
    CompleteNeed: function(itemID, url) {
        // Call the ajax method to remove the need.
        $.post(url, function(data) {
            // Update the need list item to display the need status as "Met"
            $('#' + itemID + ' div.status').html('Met');

            // Hide the Complete and Cancel buttons.
            $('#' + itemID + ' button').hide();
        });
    },

    //
    // Removes a need
    //
    CancelNeed: function(itemID, url) {
        // Call the ajax method to remove the need.
        $.post(url, function(data) {
            // Remove the user list item from the needs list.
            $('#' + itemID).fadeOut();
        });
    },

    //
    // Commits a user to a need.
    //
    CommitUser: function(itemID, url) {
        // Call the ajax method to commit the user.
        $.post(url, function(data) {
            // Update the need list item to display the user's status as "Committed."
            $('#' + itemID + ' div.userneedstatus').html('Committed');

            // Hide the Commit and Cancel buttons.
            $('#' + itemID + ' button').hide();
        });
    },

    //
    // Displays a modal dialog for the user to enter a rating.
    //
    DisplayRateUserDialog: function(itemID, url) {
        // Display modal dialog where the user can enter a rating.
        $('#rate_user_dialog').dialog('open');

        // Set the ajax post url.
        Dashboard.RateUserUrl = url;

        // Set the list item id.
        Dashboard.RateUserItemID = itemID;
    },

    //
    // Rates the user.
    //
    RateUser: function(rating, itemID, url) {
        // Call the ajax method to rate the user.
        $.post(url, { rating: rating }, function(data) {
            // Hide the list item.
            $('#' + itemID).hide();
        });
    }
}

Index = {
    Loaded: function() {
        Message.InitializeDialog();
    },

    ExpressInterest: function(needID, url) {
        // Call the ajax method to modify the user's involvement status.
        $.post(url, function() {

        });

        // Display modal dialog to send a message to the submitter of the need.
        Message.ShowNewMessageDialog(needID, null, needID);
    }
}

Message = {
    NeedID: 0,
    UserID: 0,
    ElementID: 0,

    InitializeDialog: function() {
        $('#new_message').dialog({
            bgiframe: true,
            autoOpen: false,
            height: 280,
            width: 400,
            modal: true,
            buttons: {
                Cancel: function() {
                    $(this).dialog('close');
                },
                'Send': function() {
                $.post('/Need/SendMessage', { needID: NeedID, userID: UserID, messageText: $('#message').val() });
                    $(this).dialog('close');

                    // Remove the list item from the list.
                    $('#' + ElementID).fadeOut();
                }
            }
        });
    },

    ShowNewMessageDialog: function(needID, userID, elementID) {
        NeedID = needID;
        UserID = userID;
        ElementID = elementID;
        //$('#new_message').show();
        $('#new_message').dialog('open');
    }

}