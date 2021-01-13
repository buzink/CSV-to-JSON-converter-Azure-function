# CSV-to-JSON-converter-Azure-function

This is an Azure Function that converts CSV text to JSON. It can be used standalone or as the api behind a custom connector in Power Automate.

## Setup

### Azure function

1. Create an Azure app
2. Create an Azure function called 'csv2json'in the Azure app based on the HTTP trigger template
3. Replace the code in the run.csx file with the code from that file in this repository
4. Click on get function url

### Power automate custom connector (optional)

Optionally you can use the function through a custom connector in Power Automate. 

1. In Power Automate, go to Data/Custom connectors and click new
2. Switch on the Swagger editor
3. Copy paste the configuration code from the swagger.json file in this repository to your custom connector
4. Change the url to the url copied from the Azure function

## Usage
You can set a parameter called csv in a GET query like so 
 `https://csv-to-json-xyz.azurewebsites.net/api/csv2json?csv=a%3Bb%3Bc%0D%0Ad%3Be%3Bf`

 Or POST JSON with the csv parameter like so:

`{"csv":"a;b;c\nd;e;f"}`

In both cases the function will return JSON as the body with the first line of CSV as the variable names. like so:

`[{"a":"d","b":"e","c":"f"}]`