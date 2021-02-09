package main

import (
	// db
	_ "github.com/mattn/go-sqlite3"
	"database/sql"
	
	// misc
	"fmt"
)

var (
	database *sql.DB	
)

func main() {
	database, _ = sql.Open("sqlite3", "minitwit.db") // TODO: make it reference /tmp
	defer database.Close()
	
	row, _ := database.Query("select user_id from user")
	defer row.Close()
	for row.Next() {
		var id int
		row.Scan(&id)
		fmt.Println(id)
	}	
}
