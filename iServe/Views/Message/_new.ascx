<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
	<p id="validateTips">All form fields are required.</p>

	<form>
	<fieldset>
		<label for="name">Message</label>
		<input type="text" name="Body" id="Body" class="text ui-widget-content ui-corner-all" />
		<label for="email">Email</label>
		<input type="text" name="email" id="email" value="" class="text ui-widget-content ui-corner-all" />
		<label for="password">Password</label>
		<input type="password" name="password" id="password" value="" class="text ui-widget-content ui-corner-all" />
	</fieldset>
	</form>
