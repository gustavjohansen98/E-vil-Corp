package main

import (
	// db
	_ "github.com/mattn/go-sqlite3"
	"database/sql"

	"fmt"
	"net/http"
	"html/template"
	"github.com/gorilla/mux"
)

type User struct {
	user_id int
	username string
	email string
	pw_hash string
}

var (
	user = User{1, "", "", ""}
	username string
	database *sql.DB
)

const URL = "http://localhost:10000"

// Route: /
// Method: GET
func timeline(w http.ResponseWriter, r *http.Request) {
	/*
	Shows a users timeline or if no user is logged in it will
	redirect to the public timeline.  This timeline shows the user's
	messages as well as all the messages of followed users.
	*/
	fmt.Println("We got a visitor from: " + r.RemoteAddr)
	
	if &user == nil {
		http.Redirect(w, r, URL, http.StatusPermanentRedirect)
	} else {
		data, _ := database.Query("select user_id from user")
		// var data = "" // var data = query_db()
		t := template.Must(template.ParseFiles("templates/timeline.html"))
		
		t.ExecuteTemplate(w, "public_timeline", data)
	}
}

// Route: /public
// Method: GET
func public_timeline(w http.ResponseWriter, r *http.Request) {
	// TODO
}

// Route: /<username> 
// Method: GET
func user_timeline(w http.ResponseWriter, r *http.Request) {
	// TODO
}

func main() {
	database, _ = sql.Open("sqlite3", "minitwit.db") // TODO: make it reference /tmp
	defer database.Close()
	
	router := mux.NewRouter()
	router.HandleFunc("/", timeline)
	router.HandleFunc("/public", public_timeline)
	router.HandleFunc("/" + username, user_timeline)
	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: " + URL)
	http.ListenAndServe(":10000", nil)
}
