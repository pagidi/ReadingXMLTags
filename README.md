# ReadingXMLTags from a string content

The objective of this POC is to identify XML elements from a string content and return the values.
e.g:  Customer sends information in emails as below. We have to figure out the xml elements and return as response.

# input:
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

# Using regex i could able to give the solution, but still there are some improvements needs to incorporate
    1. Validation e.g if there is no end tag reject entire input
    
# Key part of the code
        protected internal static string GetMatch(string text)
        {
            Regex RegexTagsPattern = new Regex(@"<[^>]+>[^<]*</[^>]+>");
            StringBuilder output = new StringBuilder();

            bool matchFound = false;
            foreach (var item in RegexTagsPattern.Matches(text))
            {
                matchFound = true;
                output.Append(item);
            }

            if (matchFound)
                return $"<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><root>{output.ToString()}</root>";
            else
                return string.Empty;
        }


# Created a sample api to read the xml content which is part of string content

Note: To Open the solution we need Visualstudio 2015(update 3)

we can use postman to test the api

To make things easier to test follow the below link once you run api by pressing F5

http://localhost:54900/api/parser?text=yourcontentcangohere<value>abc</value>andsomeothercontentappendedhere..

# Created a sample console app as well by adding xml content in the main method
