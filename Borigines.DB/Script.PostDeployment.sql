/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



EXEC RegisterUser 'Filip' , 'Depuydt' , 'Filip.Depuydt@test.com', 'test1234'
EXEC RegisterUser 'Mathilde' , 'Depuydt' , 'Mathilde.Depuydt@test.com', 'test1234'


UPDATE Users SET Is_Admin = 1 where  Id = 1 ;

set dateformat 'dmy'

INSERT INTO Picture (Name_picture) VALUES 
            (N'test1.jpg') ,
            (N'test2.jpg') ,
            (N'test3.jpg') ,
            (N'test4.jpg') ,
            (N'test5.jpg') ;
            


INSERT INTO Categorys (Name_Category) VALUES 
            (N'histoire') ,
            (N'vie') ,
            (N'borinage') ,
            (N'presentation') ,
            (N'objectif') ,
            (N'soirée') ,
            (N'presse');

INSERT INTO Albums (Titel , Date_Album , Id_user ) VALUES 
            (N'Albums Titel 1','12/05/2023',1),
            (N'Albums Titel 2','12/05/2023',1),
            (N'Albums Titel 3','12/05/2023',1);


INSERT INTO Album_Picture (FK_Picture,FK_Album) VALUES 
            (1,1),
            (2,1),
            (3,1),
            (4,2),
            (5,2);


INSERT INTO Content_fr (Titel,Content) VALUES 
            (N'content fr titel 1',N'content fr content 1'),
            (N'content fr titel 2',N'content fr content 2'),
            (N'content fr titel 3',N'content fr content 3');


INSERT INTO Content_en (Titel,Content) VALUES 
            (N'content en titel 1',N'content en content 1'),
            (N'content en titel 2',N'content en content 2'),
            (N'content en titel 3',N'content en content 3');


INSERT INTO Content_nl (Titel,Content) VALUES 
            (N'content nl titel 1',N'content nl content 1'),
            (N'content nl titel 2',N'content nl content 2'),
            (N'content nl titel 3',N'content nl content 3');

INSERT INTO Articles (Date_Article,Fk_category_id,FK_id_user,FK_content_fr,FK_content_en,FK_content_nl) VALUES 
            ('12/05/2023',1,1,1,1,1),
            ('12/05/2023',2,1,2,2,2),
            ('12/05/2023',3,1,3,3,3);

INSERT INTO Article_Picture (FK_Article,FK_Picture) VALUES 
            (1,1),
            (1,2),
            (1,3),
            (2,1),
            (2,2),
            (2,3),
            (3,1),
            (3,2),
            (3,2);
            


