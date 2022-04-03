# IcoBet
This is a simple project for pulling data from Xml file, storing it in Database and exposing 2 endpoints consumed by front-end. Full task:

Build an application which provides betting data. Your project will be consumed from
Front-end SPA that has two pages "Matches in next 24h" and "Match view".

Requirements:

1. You have to consume our eSports xml feed from here. You must pull it every 60 seconds and
store the data in a MSSQL database. Here is more info about the feed structure:

a. Sport - groups all Events (i.e Tournaments) currently taking place within the Sport.

b. Event - groups all Matches currently taking place within the Events (i.e Tournaments).

c. Match - contains information about participants, start date and the type of match.
There are 3 types of matches:

i. Prematch - contains prematch/pregame markets (i.e. Markets are betting
opportunities for customers – e.g Winner of the Game; Total Number of Kills;
Duration of the Game, etc.) , which are open for betting before the start date of
a match.

ii. Live - contains live markets, which are open for betting after the start date of a
match.

iii. Outright - ignore this type of matches as they are not relevant to the task at
hand.

d. Bet - these are the actual markets. We use “market” instead of “bet” everywhere in this
text. IsLive indicates whether a market is live or prematch;

e. Odd - these are the lines in a market. They can be grouped by SpecialBetValue, if it is
different than NULL;

f. Values that could change: Match.MatchType, Match.StartDate, Odd.Value

g. Note: The feed returns only currently active matches, markets and odds.

2. Once you process the feed, you have to expose two endpoints:

a. Endpoint 1:
It should return all matches starting in the next 24 hours. Minimum
required data:

i. match name

ii. match start date

iii. all active preview markets with their active odds

Preview markets are one of the following: “Match Winner “, "Map Advantage" or
"Total Maps Played". If their odds do not have a SpecialBetValue, then return all
their active odds. Otherwise, return only their first group of active odds when
grouped by SpecialBetValue.

SpecialBetValue (SBV) is used in "Map Advantage", “Total Maps Played" and
others. It indicates the condition to win. For instance, the Total Maps Played
market can have odds of under 2.5 / over 2.5 maps played, where the SBV in that
case is 2.5.

b. Endpoint 2: It should return a single match by given unique identifier. Minimum
required data:

i. match name

ii. match start date

iii. all active markets with all their active odds
