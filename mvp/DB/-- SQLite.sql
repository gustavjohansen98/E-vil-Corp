-- SQLite

SELECT count(*) FROM user;

SELECT user_id from user where username like 'Roger Histand';

SELECT user_id from user where username like 'Georgie Mathey';

select * from follower;



-- only run after tests (clears all records)
-- DELETE FROM user;
-- DELETE FROM follower;
-- DELETE FROM message;