package main

import (
	"fmt"
	"net/http"
	"github.com/gorilla/mux"
)

func main() {
	router := mux.NewRouter()
	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: http://localhost:10000")
	http.ListenAndServe(":10000", nil)
}
