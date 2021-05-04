import sqlite3
import pandas as pd
from sqlalchemy import create_engine
import sys

doc_string = """
             ITU-Minitwit Tweet Flagging Tool
             Usage:
                flag_tool <tweet_id>...
                flag tool -i
                flag tool -h
             Options:
                -h      Show this screen
                -i      Dump all tweets and authors to stdout
             """

con = create_engine("sqlite:///minitwit.db")

if len(sys.argv)==2 and sys.argv[1] == "-h":
    print(doc_string)

elif len(sys.argv)==2 and sys.argv[1] == "-i":
    query = "select * from message"
    df = pd.read_sql(query, con)
    print(df[["author_id", "text"]].to_string(index=False))

elif len(sys.argv)>=2 and sys.argv[1]!="-i" and sys.argv[1]!="-h":
    for i in range(1, len(sys.argv)):
        query = "UPDATE message SET flagged=1 WHERE message_id=" + sys.argv[i]
        try:
            c = sqlite3.connect("minitwit.db")
            cur = c.cursor()
            cur.execute(query)
            c.commit()
            c.close()
            print("Flagged entry: " + sys.argv[i])
        except:
            ("something went wrong :(")

else:
    print(doc_string)
