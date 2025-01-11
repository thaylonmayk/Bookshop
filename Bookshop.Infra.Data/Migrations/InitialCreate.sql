-- Create Assuntos table
CREATE TABLE Assuntos (
    Cod SERIAL PRIMARY KEY,
    Descricao VARCHAR(40),
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Autores table
CREATE TABLE Autores (
    Cod SERIAL PRIMARY KEY,
    Nome VARCHAR(40),
    Sobrenome VARCHAR(40),
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Canais table
CREATE TABLE Canais (
    Cod SERIAL PRIMARY KEY,
    Nome VARCHAR(40),
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create CanalPrecos table
CREATE TABLE CanalPrecos (
    Cod SERIAL PRIMARY KEY,
    CodLivro INT NOT NULL,
    CodCanal INT NOT NULL,
    Valor DECIMAL NOT NULL,
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Livros table
CREATE TABLE Livros (
    Cod SERIAL PRIMARY KEY,
    Titulo VARCHAR(40),
    Editora VARCHAR(40),
    AnoPublicacao VARCHAR(4),
    Edicao INT,
    Sinopse TEXT,
    Valor DECIMAL,
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create LivrosAutores table
CREATE TABLE LivrosAutores (
    CodLivro INT NOT NULL,
    CodAutor INT NOT NULL,
    PRIMARY KEY (CodLivro, CodAutor),
    FOREIGN KEY (CodLivro) REFERENCES Livros (Cod) ON DELETE CASCADE,
    FOREIGN KEY (CodAutor) REFERENCES Autores (Cod) ON DELETE CASCADE
);

-- Create LivrosAssuntos table
CREATE TABLE LivrosAssuntos (
    CodLivro INT NOT NULL,
    CodAssunto INT NOT NULL,
    PRIMARY KEY (CodLivro, CodAssunto),
    FOREIGN KEY (CodLivro) REFERENCES Livros (Cod) ON DELETE CASCADE,
    FOREIGN KEY (CodAssunto) REFERENCES Assuntos (Cod) ON DELETE CASCADE
);

-- Add indexes to CanalPrecos table
CREATE INDEX IX_CanalPrecos_CodLivro ON CanalPrecos (CodLivro);
CREATE INDEX IX_CanalPrecos_CodCanal ON CanalPrecos (CodCanal);

-- Add indexes to other tables
CREATE INDEX IX_Autores_Nome ON Autores (Nome);
CREATE INDEX IX_Assuntos_Descricao ON Assuntos (Descricao);
CREATE INDEX IX_Canais_Nome ON Canais (Nome);

-- Create the LivrosAutoresAssuntos view
CREATE VIEW LivrosAutoresAssuntos AS
SELECT 
    a.Cod AS AutorCod,
    a.Nome AS AutorNome,
    a.Sobrenome AS AutorSobrenome,
    l.Cod AS LivroCod,
    l.Titulo AS LivroTitulo,
    l.Editora AS LivroEditora,
    l.AnoPublicacao AS LivroAnoPublicacao,
    l.Edicao AS LivroEdicao,
    l.Sinopse AS LivroSinopse,
    l.Valor AS LivroValor,
    s.Cod AS AssuntoCod,
    s.Descricao AS AssuntoDescricao
FROM Livros l
JOIN LivrosAutores la ON l.Cod = la.CodLivro
JOIN Autores a ON la.CodAutor = a.Cod
JOIN LivrosAssuntos las ON l.Cod = las.CodLivro
JOIN Assuntos s ON las.CodAssunto = s.Cod;

-- Create indexes on the view
CREATE UNIQUE INDEX IX_LivrosAutoresAssuntos_AutorCod_LivroCod_AssuntoCod
ON LivrosAutoresAssuntos (AutorCod, LivroCod, AssuntoCod);