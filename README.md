# AirFlight
The AirFlight project allows a user to add new flights.

the Home/Index page contains the form to add new flights:
the user selects a departure airport using the autocomplete field "Depature airport" and destination airport using the autocomplete field "Destination Airport":

for each city entered, google maps api is used to get the corresponding airport located in the city. it is also used to calculate the GPS position of the airports. The user then uses the field Aircraft to select an aircraft for the flight.

the user has the possibility to choose between two aircrafts. 
each aircraft has a fuel consumption per hour: we assume that it is the fuel consumption of the aircraft in optimum conditions.
each aircraft has a takeoff effort and a Groud speed. all these fields are used to calculate the needed fuel for the flight.

So the formula to calculate the needed fuel for each flight is :

Distance between the 2 airports (in kilometers) / Groud speed (kilometers per hour) * fuel consumption (kilograms per hour) + takeoff effort.

I used google maps api to calculate the distance between the 2 airports, So an internet connection is mandatory for the process to work.

All the entered data is stored in json files located under the App_Data Folder.

the page Home/FlightsList displays all the entered data : flights with departure and destination airports, selected Aircraft model for the flight, the distance between the two airports and the needed fuel for the flight. 

The list of all entered airports whith their GPS position is also displayed in this page? 


