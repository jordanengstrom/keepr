-- CREATE TABLE keeps (
--     keepId int NOT NULL AUTO_INCREMENT,
--     name VARCHAR(20) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     userId VARCHAR(255),
--     INDEX userId (userId),
--     FOREIGN KEY (userId)
--         REFERENCES users(userId)
--         ON DELETE CASCADE,  
--     PRIMARY KEY (keepId)
-- );

-- ALTER TABLE keeps
--     ADD img VARCHAR(255);

-- ADD ITEM TO DB_TABLE
-- INSERT INTO keeps (
--   keepId,
--   name,
--   description,
--   userId,
--   link
-- ) VALUES (
--   1,
--   "testKeep1",
--   "test keep meow",
--   "12osfd"
-- );

-- -- EDIT RECORD
-- UPDATE keeps SET
--   description = "a news article"
--   WHERE keepId = 1;

-- REMOVE RECORD
-- DELETE FROM keeps WHERE keepId = 1;

-- -- GET FROM DB_TABLE
-- SELECT * FROM keeps;

-- DROP TABLE keeps