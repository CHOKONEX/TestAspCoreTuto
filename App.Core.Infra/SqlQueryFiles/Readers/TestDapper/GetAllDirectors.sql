SELECT d.Id  
    , d.Name  
    , m.DirectorId  
    , m.Id  
    , m.Name MovieName  
    , m.ReleaseYear  
FROM dbo.Directors d  
INNER JOIN dbo.Movies m  
ON d.Id = m.DirectorId