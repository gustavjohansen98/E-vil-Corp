package main

import (
	// db
	_ "github.com/mattn/go-sqlite3"
	"database/sql"

	
	// misc
	"time"
	"fmt"
	"log"
	"net/http"
	"html/template"
	"github.com/gorilla/mux"
    "crypto/md5"
    "strings"
    "encoding/hex"
	"errors"
)

type User struct {	// meta data for the user
	user_id int
	username string
	email string
	pw_hash string

	authenticated bool	// to see if user is auth and thus allowed to continue the session 
}

// will be used to manage user sessions 
var store *sessions.CookieStore

var tpl *template.Template // the tpl object will hold all the templates from /templates to be displayed 

var (
	tmpl_layout, _ = template.ParseFiles("templates/layout_go.html")
	tmpl_timeline, _ = template.ParseFiles("templates/timeline_go.html")

	user *User
	username string
	database *sql.DB
)

func init() {

	authKeyOne := securecookie.GenerateRandomKey(64)
	encryptionKeyOne := securecookie.GenerateRandomKey(32)

	store = sessions.NewCookieStore(
				authKeyOne, 
				encryptionKeyOne,
			)
	
	store.Options = &sessions.Options(
				MaxAge: 60 * 15,	// how long will we allow a session (15 minutes) 
				HttpOnly: true,		// so the session cannot be altered by js 
			)

	gob.Register(User{}) // register the User type with gob encoding package so it can be written as a session value

	tpl = template.Must(template.ParseGlob("templates/*.html")) // parse all the templates in the templates folder . these can also be found in main
}

const URL = "http://127.0.0.1:10000/"

func format_datetime(unix_timestamp int64) string {
	// Function returns date string in required format
	t := time.Unix(unix_timestamp,0)
	strDate := t.Format("2006-01-02 @ 15:04")
	return strDate
}

func gravatar_url(email string )string{
    
    size := 80
    email_striped := strings.TrimSpace(email)
    email_formated := strings.ToLower(email_striped)
    email_hash := md5.Sum([]byte(email_formated))
    email_hash_string := hex.EncodeToString(email_hash[:])
    
    
    img_url := fmt.Sprintf("http://www.gravatar.com/avatar/%s?d=identicon&s=%d",email_hash_string , size)
    return img_url 

}
// Route: /
// Method: GET
func timeline(w http.ResponseWriter, r *http.Request) {
	/*
	Shows a users timeline or if no user is logged in it will
	redirect to the public timeline.  This timeline shows the user's
	messages as well as all the messages of followed users.
	*/
	fmt.Println("We got a visitor from: " + r.RemoteAddr)
	
	if user == nil {
		http.Redirect(w, r, URL + "/public", http.StatusPermanentRedirect)
	} else { 
		//data, _ := database.Query(...) TODO
		
		tmpl_layout.Execute(w, nil)
	}
}

// Route: /public
// Method: GET
func public_timeline(w http.ResponseWriter, r *http.Request) {
	/*Displays the latest messages of all users.*/
	// data, _ := database.Query(...) TODO
	
	tmpl_layout.Execute(w, nil)
}

// Route: /<username> 
// Method: GET
func user_timeline(w http.ResponseWriter, r *http.Request) {
	var profile_user *string = nil // = query_db(...) TODO
	if profile_user == nil {
		w.WriteHeader(http.StatusNotFound)
	}
	if user != nil {
		//followd = query_db() TODO
		// render template TODO
	}
}


// Methods : GET, POST
// authenticate the user
func login(w http.ResponseWriter, r *http.Request) {
	// get the session from the client
	session, err := store.Get(r, "cookie-name")	

	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	// if an authenticated user is already in session -> redirect to public timeline
	existing_user := getUser(session)
	if auth := existing_user.authenticated; auth {
		http.Redirect(w, r, "/public", http.StatusFound)
	}

	_username := r.FormValue("username")
	// TODO: query the db to check if username exists and retrieve the password to handle wrong password
	
	// handle wrong password
	if r.FormValue("password") != "1234" { 	// this should check with db, but hardcoded for now
		
		if r.FormValue("password") == "" {	// handle emptystring as parameter
			session.AddFlash("You must enter a password")
		}

		session.AddFlash("The password you entered was incorrect")
		err = session.Save(r, w)
		if err != nil {
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		http.Redirect(w, r, "/login", http.StatusFound)
		return
	}

	// handle correct login credentials
	user := &User {	// set the user variable for the session
		username: _username,
		authenticated: true,
	}

	session.Values["user"] = user // save the user for the session

	err = session.Save(r, w)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	// everything is good to go, and the user can be redirected to public timeline
	http.Redirect(w, e, "/public")
}


// Route: '/register'
// Methods: GET, POST
func register(w http.ResponseWriter, r *http.Request) {
	return errors.New("Not yet implemented")
}

func logout(w http.ResponseWriter, r *http.Request) {
	
	session, err := store.Get(r, "cookie-name")
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	session.Values["user"] = User{} // we give this session an empty unautheticated user
	session.Options.MaxAge = -1 // the cookie will immediately be expired when we save the session

	err = session.Save(r, w)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	http.Redirect(w, r, "/public", http.StatusFound) 
}

// helper method to get the user in session
func getUser(s *sessions.Session) User {
	val := s.Values["user"]
	var user = User{}
	user, ok := val.(User)

	if !ok {
		return User{ authenticated: false }
	}

	return user
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

	s := http.StripPrefix("/static/", http.FileServer(http.Dir("./static/")))
	router.PathPrefix("/static/").Handler(s)
	
	server := &http.Server {
		Handler: 	router,
		Addr: 		"127.0.0.1:10000",
		// TODO: enforce timeouts
	}
	
	http.Handle("/", router)
	
	fmt.Println("Opened server at: " + URL)
	log.Fatal(server.ListenAndServe())
}
