# ReadingXMLTags

The objective of this POC is to identify XML elements from a string content and return the values.
e.g:  Customer sends information in emails as below. We have to figure out the xml elements and return as response.

#input:
Hi Govardhan,
Please create an expense claim for the below. Relevant details are marked up as requested…
<expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personal
card</payment_method>
</expense>
From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test
Hi Antoine,
Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan.

# expected output
cost_center : DEV002
total : 890.55
payment_method  : personal card
vendor : Viaduct Steakhouse
description : development team’s project end celebration dinner
date :Tuesday 27 April 2017


# Created a sample api to read the xml content which is part of string content

Note: To Open the solution we need Visualstudio 2015(update 3)
we can use postman to test the api


To make things easier to test follow the below link once you run api by pressing F5

http://localhost:54900/api/parser?text=Hi%20Yvaine,%20Please%20create%20an%20expense%20claim%20for%20the%20below.%20Relevant%20details%20are%20marked%20up%20as%20requested%E2%80%A6%20%3Cexpense%3E%3Ccost_centre%3EDEV002%3C/cost_centre%3E%20%3Ctotal%3E890.55%3C/total%3E%3Cpayment_method%3Epersonal%20card%3C/payment_method%3E%20%3C/expense%3E%20From:%20Ivan%20Castle%20Sent:%20Friday,%2016%20February%202018%2010:32%20AM%20To:%20Antoine%20Lloyd%20%3CAntoine.Lloyd@example.com%3E%20Subject:%20test%20Hi%20Antoine,%20Please%20create%20a%20reservation%20at%20the%20%3Cvendor%3EViaduct%20Steakhouse%3C/vendor%3E%20our%20%3Cdescription%3Edevelopment%20team%E2%80%99s%20project%20end%20celebration%20dinner%3C/description%3E%20on%20%3Cdate%3ETuesday%2027%20April%202017%3C/date%3E.%20We%20expect%20to%20arrive%20around%207.15pm.%20Approximately%2012%20people%20but%20I%E2%80%99ll%20confirm%20exact%20numbers%20closer%20to%20the%20day.%20Regards,%20Ivan%20Your
