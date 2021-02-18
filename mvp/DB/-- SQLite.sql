-- SQLite

SELECT user_id 
FROM user
WHERE username like 'Guadalupe Rumps' or username like 'Beverley Campanaro';

SELECT user_id from user where username like 'Roger Histand';

SELECT user_id from user where username like 'Georgie Mathey';

select count(*) from follower where who_id == 413 and whom_id == 420;



-- only run after tests (clears all records)
DELETE FROM user;
DELETE FROM follower;
DELETE FROM message;