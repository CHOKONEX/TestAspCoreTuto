SELECT d.Id, d.Name 
FROM dbo.Directors d  
WHERE d.Id = @Id

SELECT m.Id, m.Name MovieName, m.ReleaseYear  
FROM dbo.Directors d  
INNER JOIN dbo.Movies m ON d.Id = m.DirectorId
WHERE d.Id = @Id