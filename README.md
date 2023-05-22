1. Requirements
2. -------
3. -------
4. Create a Web app / Web service API / mobile app project (technology of your choosing). Authentication is not required2. Create a page/API to parse an RSS (https://rss.nytimes.com/services/xml/rss/nyt/World.xml). 
5. Create the following:    
6. 3a)Page/API for returning news list sorted by publication date asc/desc or title alphabetically asc/desc.
7.  Sort parameter should be part of the request. News returned should contain: title, link, description, publication date)
8. 3b) possibility to search for a keyword in the news list and return the newscontaining the searched word 
9. 3c) possibility to save the news list in a database (create datamodel - news,publisher,news_group,etc. and save to DB)  
10. 3. (Extra) Our software version number uses the following template: [Major number].[Minor number].[BugFix number][-BranchNameAndNumber]Requirement:
11.  Create a method that will return the "production version" (production version = version without the [-BranchNameAndNumber]) from a list ofversions.e.g
12.  . Input = {"2.5.0-dev.1", "2.4.2-5354", "2.4.2-test.675", "2.4.1-integration.1"}desired Output = 2.4.2-5354
