package main

import (
	"fmt"
	"net/http"
	"github.com/gorilla/mux"
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
	router := mux.NewRouter()
	router.HandleFunc("/", timeline)
	router.HandleFunc("/public", public_timeline)
	router.HandleFunc("/" + username, user_timeline)
	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: http://localhost:10000")
	http.ListenAndServe(":10000", nil)
}
