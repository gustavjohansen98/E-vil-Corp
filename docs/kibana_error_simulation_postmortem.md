# Postmortem - Group e

The group of five separated into a dev team, consisting of four and an ops team consisting of one. The ops team developer then planted a single bug in a non specified place in the program making sure the dev team developers didn’t know any details about the bug’s effect or location.

The bug was an input parameter in the input of AddMessage method in the MessageRepository of the API, in which an optional input parameter named flagged2 was set to the empty string. In the code if the flagged2 string was different from the null value the program would throw an error, meaning that everytime that AddMessage was called flagged2 has to be set to null if the program was to function normally and if it wasn’t set it would default to the empty string.

The dev team then by only using the Kibana logging tool tried to find the bug. Here the group could see a lot of bugs and by using the timestamp of the bug and in which method it originated in the code, the group then found out that the bug was planted within the MessageRepository in the AddMessage method. The dev team then looked at the code for the method and found the mentioned bug and was to resolve it.

Hence the group Kibana setup could be used to at least find errors in the code.
