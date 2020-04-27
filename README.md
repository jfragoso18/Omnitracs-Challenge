![Omnitracs](https://www.omnitracs.com/themes/custom/themekit/logo.svg)
# Omnitracs-Challenge


## Create a Windows Service with C#
<ol>
<li>Every 5 minutes, the service should log the current weather in Dallas, TX to a CSV file (use any free weather API they like)</li>
  <h4> The file format should be: </h4> 
 <li>Temperature (numeric value) </li>
 <li>Units (C/F) </li>
 <li>Precipitation (true/false) </li>
 <li>Upload the solution to GitHub and share with Daniel </li>
 <li>Bonus points if the solution runs successfully in debug mode </li>
</ol>
 

### Windows Service Installation:  
#### Open the VStudio Dev Tools with privileges, then type: 
```
installutil Omnitracs.exe
```

#### Don't forget to initialize the service from Services
