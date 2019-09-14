<h1><b> CONSUL HEALTH CHECK </b></h1>

<h3> Okay.. So what the heck does this program do? </h3>
This is a simple health checker for checking registered APIs in a service of a specific node. The application can be configured to do a simple health check of all the registered APIs in a service, given a specific time, if any of the APIs fails to respond to the health checker, the HC will automatically deregister that API from that service. 

<h3> Why is removing a failing API from a service is important? </h3>
Consul is used as a service discovery tool by an API Gateway(Ocelot). Therefore we have to make sure that those services registered to Consul is actually available. Failure to do so will result the API Gateway to call end point of API servers which may not be available(Technical problem?), thus resulting an error. 

<h3> How to use this health Checker? </h3>
The user is required to provide 3 key elements in a single argument,

1. Consul Host Name
2. Service Name
3. Interval(ms)

Eg: <b> dotnet ConsulHealthChecker.dll "localhost:5555;Test-API;1000" </b>
