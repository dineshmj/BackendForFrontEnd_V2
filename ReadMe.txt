1) Technology Stack:

	a) Duende IdentityServer 8 based IDP that runs on ASP.NET Core 10.
		- Has its own SQLite-based user database (see the .db file under `UserDB` folder in the IDP project).
		- Supports OpenID Connect and OAuth 2.1 protocols.
		- Grant type: Authorization Code Flow with PKCE.
		- Supports refresh tokens for Identity Tokens that are about to expire.
		- Supports role-based access control (R-BAC) by embedding role claims onto the ID Token and Access Token.
		- Supports User Consent page.

	b) Master application: Platform Administration System (PAS), which is a Shell SPA Frontend
		- Runs on Next.js 14 with app directory structure.
		- Uses ASP.NET Core 10 for "Backend for Frontend" (BFF).

	c) Microservices:

		1) Products Microservice
			- Frontend:
				- SPA: Next.js 14 with app directory structure.
				- BFF: ASP.NET Core 10 with REST edge APIs.
			- Backend / App Tier:
				- API: ASP.NET Core 10 with REST response.

		2) Orders Microservice
			- Frontend: 
				- SPA: Next.js 14 with app directory structure.
				- BFF: Nest.js 10 with REST edge APIs.
			- Backend / App Tier:
				- API: ASP.NET Core 10 with GraphQL response.

		3) Payments Microservice (yet to be implemented)
			- Frontend:
				- SPA: React.js 18.
				- BFF: Python / Flask BFF server with REST response.
			- Backend / App Tier:
				- API: Nest.js 10 with GraphQL response.

2) What to do after cloning the repository:
	a) Open the PS1 script file "CompileAndExportBFFClients.ps1" at repo root folder inside PowerShell ISE and run it. This will:
		1) trigger running the "npm install" command for:
			- PAS Shell BFF Frontend (Next.js)
			- Products Microservice SPA Frontend (Next.js)
			- Orders Microservice SPA Frontend (Next.js)
			- Orders Microservice BFF Server (Nest.js)

		2) trigger the "Build and export" task by running the command "npm run export" on the NextJS SPA projects, which builds static files in the "out" folder under the "app" folder under their respective host ASP.NET Core BFF projects.
			- PAS Shell SPA Frontend
			- Products Microservice SPA Frontend.

	b) Now, it is time to run the "Orders" SPA, BFF and App!
		- Invoke a Visual Studio Code IDE at "Orders" Microservice BFF.Web folder:
		- Open Terminal 1 at location "BFF.Web", and run the ".\buildnow.bat" to run the Nest.js BFF application.
		- Open Terminal 2 at location "BFF.Web\client-app" and run ".\buildnow.bat" to run the NextJS SPA application.

	b) Open the "FW.PAS.sln" solution file in Visual Studio 2025 IDE.

		- Right-click on the solution node in the Solution Explorer and select "Restore NuGet Packages".
		- Right-click on the solution node in the Solution Explorer and choose "Configure Startup Projects...", and ensure that "Multiple startup projects" is selected with the following order:

			1) IDP - Start - self-hosted.
			2) Products Microservice API - Start - IIS Express.
			3) Orders Microservice API - Start - IIS Express.
			4) Products Microservice BFF Frontend - Start - IIS Express.
			5) (Note! The Orders Microservice BFF Frontend is a Nest.js application that must be started separately in VS Code, which you are doing anyway above (- Right-click step)).
			6) PAS Shell BFF Frontend - Start - IIS Express.

	c) Run the "FW.PAS.sln" solution in Debug mode (F5).

		- Since the IDP project is "self-hosted", a console window will open for the IDP project, showing logs.
		- The IDP shall be running at the default Duende IdentityServer port, which is 44392 (https://localhost:44392).

	d) Open the web browser, and ensure that cookies and history ("from all time") are cleared before starting the testing.

	e) Navigate to the URL of the PAS Shell BFF Frontend application at "https://localhost:44367". The expected behavior:

		- Since "authentication cookie" is not present in the request from the web browser, the Shell BFF server will redirect the browser to the IDP login page at "https://localhost:44392".
		- The browser gets a 302 - Redirect response from the Shell BFF server, and navigates to the IDP login page.
		- The user is presented with the IDP login page.
		- The user enters the credentials for "JuliaRob" user:
			- Username: JuliaRob
			- Password: JuliaRob123
		- After successful login, the IDP presents the "consent" page to the user, asking for consent to share profile data with the Shell BFF application.
		- After giving consent, the IDP redirects the browser back to the Shell BFF's "callback" endpoint with an authorization code.
		- The browser gets a 302 - Redirect response from the IDP, and navigates to the Shell BFF's "callback" endpoint.
		- The browser navigates to the Shell BFF's "callback" endpoint with the "authorization code" in the query string (which is PKCE protected).
		- The Shell BFF server exchanges the authorization code for ID Token and Access Token from the IDP.
		- Once the ID and Access Tokens are received, the Shell BFF extracts the "claims" and "scopes" from the tokens, and creates an authentication cookie for the user.
		- The Shell BFF server returns a 200 - OK with the contents of the landing page along with the authentication cookie in the response.
		- The browser displays the landing page of the Shell BFF Frontend application.
			* In all subsequent requests from the browser to the Shell BFF server, the authentication cookie is sent along with the request, and the user is authenticated and authorized based on the roles present in the claims.
		- The left-pane of the Shell BFF Frontend application shows the available Microservices and Management Areas based on the user's roles.
		- When clicked on a menu item, a JavaScript in the Shell BFF application sets an iFrame's "src" attribute to the URL of the respective Microservice BFF Frontend application.
			* This Microservice BFF Frontend URL will try to perform a silent authentication using the existing session at the IDP (since the IDP cookie is also present in the browser).
		- Once the silent authentication is successful, the Microservice BFF Frontend application creates its own authentication cookie for the user, and presents the page requested in the iFrame.

	f) The following are the logout behavior:
		- When the user clicks on the "Sign Out" button in the Shell BFF Frontend application, the Shell BFF server clears its authentication cookie, and redirects the browser to the IDP's "end session" endpoint.
		- Before doing the above step, the Shell BFF server also sends a back-channel logout request to all involved Microservice BFF Frontend applications to do a "silent-logout, and clear their authentication cookies for the user.
		- The IDP clears its session cookie, and presents a logout confirmation page to the user.
		- After confirming logout, the IDP redirects the browser to the login page.

3) Final touches required (not urgent, only after all modules are implemented):
   - Teraform scripts must be written to provision all required infrastructure in a cloud provider (e.g., Azure, AWS, GCP).

4) Troubleshooting:
	* Unresponsive modules (such as, the Products links, when clicked, take a long time, but nothing gets rendered inside the iFrmae):

		- Come out of Visual Studio 2026.
		- Go to the repository root folder.
		- Locate the ".vs" folder over there.
		- SHIFT+Delete that folder.
		- Invoke the FW.PAS.sln solution in Visual Studio 2026 again.
			! The "Configure Startup projects ..." would have changed by now.
		- Right-click on the solution node in Solution Explorer, and go to "Configure Startup Projects ... " menu.
		- Re-specify the dependency order of projects to be started (just click on the appropriate radio button).
		- Clean the solution.
		- Run the solution.

	* "Orders" Microservice API not contactable:

		- The "NestJS" BFF type-script code takes the "Orders Microservice API" URL from the .env file.
		- However, it has been observed that even though the "launchSettings.json" file of the "Orders" Microservice GraphQL API is set to a particular HTTPS port number,
		  while running the Visual Studio 2026 solution "FW.PAS.sln", Visual Studio 2026 IDE changes the port to some other number without a specific reason.
		- This will cause the "NestJS" BFF of the "Orders" Microservice web frontend to result in "404 - Not Found" response from the Orders Microservice API.
		- What to do in this context:
			1) Check what is the new port number in the launchSettings.json file of the "Orders" Microservice API. Note down this port number.
			2) Come to the .env file of the "NestJS" BFF of the "Orders" Microservice web front-end, and change accordingly (the last line in the .env file - `ORDERS_MICROSERVICE_API_URL=https://localhost:44380`).
			3) If the NestJS BFF is already running in a Terminal (under .\BFF.Web\ folder), CTRL+C it, and do a rerun by entering ".\buildnow.bat".
			4) Test the Orders Microservice menu item in the PAS application.

4) Important Gotchas and relevant URLs:
	- Cookie names of BFF projects must begin with "__". If not, there can be issues with signing in and out from Duende IDP server.
		- Shell BFF UI cookie name: "__PAS-Shell-Host-bff"
		- Products BFF UI cookie name: "__PAS-Microservice-Products-Host-bff"