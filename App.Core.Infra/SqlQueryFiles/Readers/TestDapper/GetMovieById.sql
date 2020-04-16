SELECT m.Id  
    , m.Name  
    ,d.Name AS DirectorName  
    , m.ReleaseYear  
FROM Movies m  
INNER JOIN Directors d  
ON m.DirectorId = d.Id  
WHERE m.Id = @Id