package main

import (
	// db
	_ "github.com/mattn/go-sqlite3"
	"database/sql"

	"fmt"
	"log"
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
	tmpl_timeline = template.Must(template.ParseFiles("templates/layout_go.html"))

	user = User{1, "", "", ""}
	username string
	database *sql.DB
)

const URL = "http://localhost:10000/"

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
		//data, _ := database.Query("select user_id from user")
		// var data = "" // var data = query_db()
		
		tmpl_timeline.Execute(w, nil)
	}
}

// Route: /public
// Method: GET
func public_timeline(w http.ResponseWriter, r *http.Request) {
	/*Displays the latest messages of all users.*/
	// data, _ := database.Query("select user_id from user")
	// var data = "" // var data = query_db()
		
	tmpl_timeline.Execute(w, nil)
	
	//http.ServeFile(w, r, "templates/timeline.html")
	
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

	s := http.StripPrefix("/static/", http.FileServer(http.Dir("./static/")))
	router.PathPrefix("/static/").Handler(s)
	
//	router.PathPrefix("/static/").Handler(http.StripPrefix("/static/", http.FileServer(http.Dir("/static/"))))
	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: " + URL)
	log.Fatal(http.ListenAndServe(":10000", nil))
}
