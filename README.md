# I.T. Asset Management
Asset Management demo project for an I.T. Company

## Technologies used: 
   * Front End  - Vue.js version 2.6.10
   * Back End   - Visual C#/Asp.net version 2013
   * RDBMS      - MS SQL 2016

Git clone from https://github.com/avtividad/asset-management.git

 
## Run the frontend app only ##

Go to frontend directory and double-click on “index.html” 

### Sample Data

[Rest Api data on the web](https://www.restapii.xyz/api/assets)



## Run the frontend + backend app  ##

1. Go to “backend” directory and open “restapii2.sln”
2. Modify the “connectionString” in web.config to point to your DB:
    ##### connectionString="Data Source=localhost;Initial Catalog=restapii2DB;User Id=sa;Password=sa;Integrated Security=True"
3. Build and run. 
4. Go to “frontend” directory and double-click on “index.localhost.html” 

### Sample Data

[Rest Api data on localhost](http://localhost:33000/api/assets)

#### Prerequisites: 
   * Visual Studio 2013 or later
   * MS SQL 2012 or later

# Screenshots
##### Assets List
![alt tag](screenshots/screenshot-list.JPG)

##### Edit mode
![alt tag](screenshots/screenshot-edit.JPG)
