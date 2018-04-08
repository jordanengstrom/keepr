-- CREATE TABLE vaults (
--     vaultId int NOT NULL AUTO_INCREMENT,
--     name VARCHAR(20) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     userId VARCHAR(255),
--     INDEX userId (userId),
--     FOREIGN KEY (userId)
--         REFERENCES users(userId)
--         ON DELETE CASCADE,  
--     PRIMARY KEY (vaultId)
-- );

-- ADD ITEM TO DB_TABLE
-- INSERT INTO vaults (
--   vaultId,
--   name,
--   description,
--   userId
-- ) VALUES (
--   1,
--   "testVault1",
--   "vault description 1",
--   "12osfd"
-- );

-- -- EDIT RECORD
-- UPDATE vaults SET
--   name = "updated name"
--   WHERE vaultId = 1;

-- REMOVE RECORD
-- DELETE FROM vaults WHERE vaultId = 1;

-- -- GET FROM DB_TABLE
-- SELECT * FROM vaults;