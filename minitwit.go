package main

import (
	// db
	"github.com/mattn/go-sqlite3"
	"database/sql"
	
	// misc
	"fmt"
	"net/http"
	"github.com/gorilla/mux"
)


var (
	database *sql.DB	
)

var (
	username string
)

// Route: /
// Method: GET
func timeline(w http.ResponseWriter, r *http.Request) {
	// TODO
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
	
	row, _ := database.Query("select user_id from user")
	defer row.Close()
	for row.Next() {
		var id int
		row.Scan(&id)
		fmt.Println(id)
	}
  
	router := mux.NewRouter()
	// Handlefunc("<URL path>", <handler aka method aka controller>)
	router.HandleFunc("/", timeline)
	router.HandleFunc("/public", public_timeline)
	router.HandleFunc("/" + username, user_timeline)
	router.HandleFunc("/login", login).Methods("GET", "POST")
	router.HandleFunc("/register", register).Methods("GET", "POST")
	router.HandleFunc("/logout", logout)

	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: http://localhost:10000")
	http.ListenAndServe(":10000", nil)

}


// Route: '/login'
// Methods : GET, POST
func login(w http.ResponseWriter, r *http.Request) {

}


// Route: '/register'
// Methods: GET, POST
func register(w http.ResponseWriter, r *http.Request) {

}


// Route: '/logout'
func logout(w http.ResponseWriter, r *http.Request) {

}
