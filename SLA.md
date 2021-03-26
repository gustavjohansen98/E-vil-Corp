###Service Level Agreement of E-vil Corp.’s Minitwit application

During the Term of the agreement under which E-vil Corp. has agreed to provide Minitwit API to Customer , the Covered Service will provide a Monthly Uptime Percentage (MUP) over 95%. 
If the customer meets it’s obligations defined in this SLA and Evil corp fails to provide the  MUP mentioned before the Customer is eligible for Credit Refund defined below. The only possible reason for the Customer to Receive Credit refund if E-vil Corp. does not meet the minimum stated MUP. 

###The Credit Refund must be requested

The Customer has to request the Credit Refund within 40 days from the down time incident and send a report about it to the E-vil Corp.’s Customber service. The report must contain the following information: The Customer service ID the date and time when the error occurred and the error messages. 


###Definitions
**Incident**: occurrence of at least minimum unit of Downtime
**Back-off requirement**: when an error occurs, the Customer must wait at least one second before issuing a new request. When consecutive requests fail, the Customer must wait X seconds before submitting a new request, where X follows the Fibonacci sequence (1, 1, 2, 3, 5, 8, etc.) up to and including 34.
This SLA covers the following services:
    Minitwit API
**Downtime** means more than 10% Error Rate. 
**Downtime Period** is defined as one or more minimum units of consecutive Downtime. The minimum unit of downtime is 45 seconds, partial downtime shorter than the minimum unit is not counted towards Downtime Periods.
**Error** means that a request returns an HTTP error with Error Code 4xx or that a request fails to return a Success Code within 300 seconds of request submission.
**Error rate**: is defined as the number of Errors divided by the number of valid requests during a minimum unit of Downtime.

###Monthly Uptime Measure
Monthly Uptime Percentage is calculated as (all valid requests - all failed requests) / all valid requests. The time frame for monthly uptime is measured as the time between the first and last day of a calendar month (that is 1st-31st January, 1st-28th February, etc.)

###Refunds
The Customer is eligible for refunds originating from service downtime according to the table below:
MUP | Refund %
----------|-----------
>95% | 0
90-95% | 10
80-90% | 25
<80% | 50

###SLA exclusions
The SLA applies for all covered Services and their full functionality, unless explicitly declared _inappliclable_ in their respective Services documentation.

