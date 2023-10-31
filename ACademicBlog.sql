USE master;
GO
Drop Database AcademicBlogDB;
GO
Create Database AcademicBlogDB;
GO
USE AcademicBlogDB;

--- Role Table ---
Insert into Role (Id, Name) Values ('662396ee-6231-4d9f-b06e-deac96ec96cf', 'Student');
Insert into Role (Id, Name) Values ('d74b4521-ab22-428e-90d6-591fd0da23b1', 'Lecturer');
Insert into Role (Id, Name) Values ('a9c52b1e-67b1-4834-9b0a-e8bfef9eab1c', 'Admin');

--- Account Table ---
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId, AccountFieldMappingId) Values
('50a086dd-9222-4d82-a907-60950b31d9e1', 'Admintrator', 'Admintrator', '@@admin@@', 'ACTIVE', 'admin@AcademicBlog.com', 0, 'a9c52b1e-67b1-4834-9b0a-e8bfef9eab1c', null);

Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId, AccountFieldMappingId) Values
('a4cd997e-5976-4cbb-b45d-b5ebded9782f', 'Alex', 'Alexsander', '12345', 'ACTIVE', 'AlexanderWilliams@ACademicBlog.org', 1, '662396ee-6231-4d9f-b06e-deac96ec96cf', null);
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId, AccountFieldMappingId) Values
('1e92a1b2-bdcb-4521-bc6f-818b5bd53b98', 'Strange', 'DoctorStrange', '12345', 'ACTIVE', 'DoctorStrange@MCU.marvel', 1, '662396ee-6231-4d9f-b06e-deac96ec96cf', null);
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('f8bb0e19-b646-400e-94ad-8a06f40d3b01', 'Emily', 'EmilyJohnSon', '12345', 'ACTIVE', 'EmilyJohnson@ACademicBlog.org', 1, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('de317fd1-ed36-40d6-ac74-5aec009da2ca', 'Harly', 'HarlyQuuin', '12345', 'ACTIVE', 'HarlyQuuin@DCU.dc', 1, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('c5771dfb-21b3-4061-88a7-a6aae4980af2', 'John', 'JohnWick', '12345', 'ACTIVE', 'JonhWick@JonhWick.jw', 1, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId, AccountFieldMappingId) Values
('7d696a6f-dd79-4b56-9863-e72d15c3faa6', 'Sophia', 'Sophia Davis', '12345', 'INACTIVE', 'SophiaDavis@ACademicBlog.org', 5, '662396ee-6231-4d9f-b06e-deac96ec96cf', null);
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('20b7459c-4bf7-4a89-b91f-107369043d92', 'Benjamin', 'BenjaminThompson', '12345', 'INACTIVE', 'BenjaminThompson@ACademicBlog.org', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('b365be3d-9559-4e50-8fe9-36a02482087b', 'Manhaten', 'DoctorManhten', '12345', 'INACTIVE', 'DoctorManhaten@DCEU.dc', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('ca31764c-ab67-4c97-9583-05a0ab9fc9e6', 'Tony', 'TonyStark', '12345', 'INACTIVE', 'TonyStark@MCU.marvel', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId, AccountFieldMappingId) Values
('673d32dd-c157-4ab1-93e1-1315ea6b136f', 'Charlotte', 'CharlotteEvans', '12345', 'BANNED', 'CharlotteEvans@ACademicBlog.org', 5, '662396ee-6231-4d9f-b06e-deac96ec96cf', null);
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('2167bd65-cc50-4956-aa44-d07d8ec20854', 'Peter', 'PeterParker', '12345', 'BANNED', 'PeterParker@ACademicBlog.org', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('6a84948b-a883-4b11-943d-a8a602796d52', 'Peter', 'PeterParker', '12345', 'BANNED', 'PeterParker@MCU.marvel', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');
Insert into Account (Id, UserName, Name, Password, Status, Gmail, NumberOfBlogs, RoleId) Values
('725c507f-60ed-4bca-bbe8-7545dc7b7849', 'Robin', 'TitanRobin', '12345', 'BANNED', 'TintanRobin@DCEU.dc', 5, 'd74b4521-ab22-428e-90d6-591fd0da23b1');

--- Acount Field Mapping Table ---
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('7e893b0c-7b3b-4d7b-96cf-322482df9153', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06', 'f8bb0e19-b646-400e-94ad-8a06f40d3b01');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('39422080-e301-47ab-add9-f78ed169dd1a', 'ca23317f-e026-4e5a-b8aa-e5f80027aced', 'de317fd1-ed36-40d6-ac74-5aec009da2ca');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('79f00d59-15f5-4cab-938b-38bbd511b8af', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d', 'c5771dfb-21b3-4061-88a7-a6aae4980af2');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('c44267e4-ee7e-4943-a061-13745c3bf685', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06', 'b365be3d-9559-4e50-8fe9-36a02482087b');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('8ea8a013-540b-463b-b636-057fcdd840d7', 'ca23317f-e026-4e5a-b8aa-e5f80027aced', 'ca31764c-ab67-4c97-9583-05a0ab9fc9e6');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('eedd182f-4aef-4e77-b6e8-0f4be1284303', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d', '20b7459c-4bf7-4a89-b91f-107369043d92');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('c6d15934-3067-4893-801d-f5f3e7ec7ed0', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06', '2167bd65-cc50-4956-aa44-d07d8ec20854');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('9f10cc36-608b-40ed-b8e8-91a3d7897a9e', 'ca23317f-e026-4e5a-b8aa-e5f80027aced', '6a84948b-a883-4b11-943d-a8a602796d52');
Insert into AccountFieldMappings (Id, FieldId, AccountId) Values 
('a1caddcf-8da4-4b99-a957-9015721c8521', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d', '725c507f-60ed-4bca-bbe8-7545dc7b7849');

UPDATE Account
SET AccountFieldMappingId = '7e893b0c-7b3b-4d7b-96cf-322482df9153'
WHERE Id = 'f8bb0e19-b646-400e-94ad-8a06f40d3b01';
UPDATE Account
SET AccountFieldMappingId = '39422080-e301-47ab-add9-f78ed169dd1a'
WHERE Id = 'de317fd1-ed36-40d6-ac74-5aec009da2ca';
UPDATE Account
SET AccountFieldMappingId = '79f00d59-15f5-4cab-938b-38bbd511b8af'
WHERE Id = 'c5771dfb-21b3-4061-88a7-a6aae4980af2';
UPDATE Account
SET AccountFieldMappingId = 'c44267e4-ee7e-4943-a061-13745c3bf685'
WHERE Id = 'b365be3d-9559-4e50-8fe9-36a02482087b';
UPDATE Account
SET AccountFieldMappingId = '8ea8a013-540b-463b-b636-057fcdd840d7'
WHERE Id = 'ca31764c-ab67-4c97-9583-05a0ab9fc9e6';
UPDATE Account
SET AccountFieldMappingId = 'eedd182f-4aef-4e77-b6e8-0f4be1284303'
WHERE Id = '20b7459c-4bf7-4a89-b91f-107369043d92';
UPDATE Account
SET AccountFieldMappingId = 'c6d15934-3067-4893-801d-f5f3e7ec7ed0'
WHERE Id = '2167bd65-cc50-4956-aa44-d07d8ec20854';
UPDATE Account
SET AccountFieldMappingId = '9f10cc36-608b-40ed-b8e8-91a3d7897a9e'
WHERE Id = '6a84948b-a883-4b11-943d-a8a602796d52';
UPDATE Account
SET AccountFieldMappingId = 'a1caddcf-8da4-4b99-a957-9015721c8521'
WHERE Id = '725c507f-60ed-4bca-bbe8-7545dc7b7849';

--- Blog Table ---
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b4','images/github.png', 'What is github', 'GitHub is a for-profit company that offers a cloud-based Git repository hosting service. Essentially, it makes it a lot easier for individuals and teams to use Git for version control and collaboration. GitHub is interface is user-friendly enough so even novice coders can take advantage of Git. Without GitHub, using Git generally requires a bit more technical savvy and use of the command line. Additionally, anyone can sign up and host a public code repository for free, which makes GitHub especially popular with open-source projects.','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, '6844C8D9-8AC5-4C3D-88D6-76E79F0F0A8A', null, '1E92A1B2-BDCB-4521-BC6F-818B5BD53B98', 'Good Blog','GitHub is a for-profit company that offers a cloud...');
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b5','images/eager.png', 'Eager Loading of Related Data', 'You can use the Include method to specify related data to be included in query results. In the following example, the blogs that are returned in the results will have their Posts property populated with the related posts.You can drill down through relationships to include multiple levels of related data using the ThenInclude method. The following example loads all blogs, their related posts, and the author of each post.','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, '6844C8D9-8AC5-4C3D-88D6-76E79F0F0A8A', null, 'A4CD997E-5976-4CBB-B45D-B5EBDED9782F', 'Good Blog','You can use the Include method to specify related data to be included in query results. In the following example, the blogs that are returned in the results will have their Posts property populated with the related posts....');
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b6','images/react.png', 'ReactJs', 'React lets you build user interfaces out of individual pieces called components. Create your own React components like Thumbnail, LikeButton, and Video. Then combine them into entire screens, pages, and apps.React components are JavaScript functions. Want to show some content conditionally? Use an if statement. Displaying a list? Try array map(). Learning React is learning programming.','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, 'AEF378EE-EAC6-4083-B6F2-01AA11C39BE7', null, '725C507F-60ED-4BCA-BBE8-7545DC7B7849', 'Good Blog','This markup syntax is called JSX. It is a JavaScript syntax extension popularized by React. Putting JSX markup close to related rendering logic makes React components easy to create, maintain, and delete....');
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b7','images/react.png', 'ReactJs', 'React lets you build user interfaces out of individual pieces called components. Create your own React components like Thumbnail, LikeButton, and Video. Then combine them into entire screens, pages, and apps.React components are JavaScript functions. Want to show some content conditionally? Use an if statement. Displaying a list? Try array map(). Learning React is learning programming.','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, 'AEF378EE-EAC6-4083-B6F2-01AA11C39BE7', null, '725C507F-60ED-4BCA-BBE8-7545DC7B7849', 'Good Blog','This markup syntax is called JSX. It is a JavaScript syntax extension popularized by React. Putting JSX markup close to related rendering logic makes React components easy to create, maintain, and delete....');
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b8','images/marketing.png', 'Marketing', 'The AMA’s definitions of marketing and marketing research are reviewed and reapproved/modified regularly by a panel of five scholars who are active researchers.Marketing research is the function that links the consumer, customer, and public to the marketer through information—information used to identify and define opportunities and problems; generate, refine, and evaluate actions; monitor performance; and improve understanding of it as a process. It specifies the information required to address these issues, designs the method for collecting information, manages and implements the data collection process, analyzes the results, and communicates the findings and their implications.','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, 'D58B7C9C-98BE-46E6-A661-1E0210BD02CE', null, '673D32DD-C157-4AB1-93E1-1315EA6B136F', 'Good Blog','The AMA’s definitions of marketing and marketing research are reviewed and reapproved/modified regularly by a panel of five scholars who are active researchers...');
Insert into Blog (Id, Thumbnal_Url, Title, Description, Status, ReviewDateTime, CreatedTime, UpdatedTime, [View] , CategoryId, ReviewerId, AuthorId, ReviewFromReviewer,ShortDescription) Values
('eb46d425-4511-4a98-a8ac-80eb8f5352b9','images/pronounciation.jpg', 'How to improved pronouciation', 'Listen and write :Take a short clip from a song, film, show or audio file. Write down the words that you hear and replay it until you have got them all. Play it at 0.75x or 0.5x speed if you need to.Then compare your version with the lyrics, transcript or subtitles. Did you get it exactly? Note any differences. This really helps you to focus on particular pronunciation features.Finally, say the words yourself, copying the same sounds that you heard. Repeat the practice until you can pronounce the sounds smoothly and comfortably.2. Speak and check : This is the reverse of the previous tip. Now, you say the words and let a dictation app or website write down what you say.If it writes down what you said correctly, you must have pronounced it well! But if it does not check those misunderstood words because you might not have pronounced them clearly. (Note, however, that dictation programs do sometimes make mistakes.)For this tip, you will need a dictation app or website. Many phones have voice dictation functions. You can also use websites such as (make sure you set the language to English).','PENDING', '2023-10-26','2023-10-25','2023-10-26', 0, '5D57EF10-4CDC-464E-AD52-8D766EF136F8', null, 'A4CD997E-5976-4CBB-B45D-B5EBDED9782F', 'Good Blog','Take a short clip from a song, film, show or audio file. Write down the words that you hear and replay it until you have got them all. Play it at 0.75x or 0.5x speed if you need to.');




--- Category Table ---
Insert into Category (Id, Name, Status, FieldId) Values ('aef378ee-eac6-4083-b6f2-01aa11c39be7', 'Font-End', 'ACTIVE', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06');
Insert into Category (Id, Name, Status, FieldId) Values ('6844c8d9-8ac5-4c3d-88d6-76e79f0f0a8a', 'Back-End', 'ACTIVE', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06');
Insert into Category (Id, Name, Status, FieldId) Values ('6cbc7902-573b-42b9-94a0-a187290a500c', 'SAP', 'ACTIVE', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06');
Insert into Category (Id, Name, Status, FieldId) Values ('eba9f435-b36d-4317-9507-ec89955df639', 'BA', 'ACTIVE', 'e6661e13-4418-45d3-b96e-e69d4d0a5f06');
Insert into Category (Id, Name, Status, FieldId) Values ('d58b7c9c-98be-46e6-a661-1e0210bd02ce', 'Marketing', 'ACTIVE', 'ca23317f-e026-4e5a-b8aa-e5f80027aced');
Insert into Category (Id, Name, Status, FieldId) Values ('0043c257-b849-4770-bf7f-be00a33fc383', 'Economics', 'ACTIVE', 'ca23317f-e026-4e5a-b8aa-e5f80027aced');
Insert into Category (Id, Name, Status, FieldId) Values ('709707bc-9d4a-4ce4-a95c-526bc7fd262b', 'Finance', 'ACTIVE', 'ca23317f-e026-4e5a-b8aa-e5f80027aced');
Insert into Category (Id, Name, Status, FieldId) Values ('5d57ef10-4cdc-464e-ad52-8d766ef136f8', 'English', 'ACTIVE', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d');
Insert into Category (Id, Name, Status, FieldId) Values ('13f138da-15de-49e1-9cba-db7f64f11c56', 'Japan', 'ACTIVE', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d');
Insert into Category (Id, Name, Status, FieldId) Values ('8a8bdb56-b488-4460-b922-9cae647bc027', 'China', 'ACTIVE', '908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d');

--- Field Table ---
Insert into Field (Id, Name, Status) Values ('e6661e13-4418-45d3-b96e-e69d4d0a5f06', 'Software Engineering ', 'ACTIVE');
Insert into Field (Id, Name, Status) Values ('ca23317f-e026-4e5a-b8aa-e5f80027aced', 'Bussiness', 'ACTIVE');
Insert into Field (Id, Name, Status) Values ('908fe5a2-f1d0-44e9-a4c2-e7f53fa2cc0d', 'Language', 'ACTIVE');