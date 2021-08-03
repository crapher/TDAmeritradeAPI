
# TDAmeritradeAPI

TDAmeritrade client library for .NET developers.
This is an improved version of the original Library with full streaming support and automatic access and refresh token update.

To achieve the automatic access and refresh token update it is needed to generate the initial tokens file.

# Configuration File Genetation (First Time)

These are the steps to generate the file with the tokens.

1) Open you browser and enter the URL 
    https://auth.tdameritrade.com/auth?response_type=code&redirect_uri={REDIRECT_URL}&client_id={CONSUMER_KEY}%40AMER.OAUTHAP
    Replace the REDIRECT_URL with the URL configured in the application
    Replace the CONSUMER_KEY with the Consumer Key (It is in the Keys tab in the TD Ameritrade app)
2) After this, you will be redirected to the REDIRECT_URL.  The URL querystring contains a code parameter, copy the value (Do NOT copy the code=)
3) Open the URL https://url-decode.com/, paste the value in the white box and click the button URL Decode.  This is going to give us the URL decoded value that will be used in the next step.
4) Open the URL https://developer.tdameritrade.com/authentication/apis/post/token-0.  Enter the following values:
	a) grant_type: authorization_code
	b) access_token: offline
	c) code: The URL decoded value generated in step 3
	d) client_id: CONSUMER_KEY
	e) redirect_url: REDIRECT_URL
5) Click SEND
6) If everything is correct, you should receive a 200 successful and the response tab should have a json string.  Copy the whole json string to a file. (This file is the one we will be used)

There is some additional information about the authentication process in https://developer.tdameritrade.com/content/authentication-faq 

# API (https://developer.tdameritrade.com/apis) 

This is the list of the methods exposed by the API client.

## Accounts and Trading

### Orders
- Cancel Order (Implemented)
- Get Order (Implemented)
- Get Orders By Path (Implemented)
- Get Orders By Query (Implemented)
- Place Order (Implemented)
- Replace Order (Implemented)

### Saved Orders
- Create Saved Order (Implemented)
- Delete Saved Order (**Pending**)
- Get Saved Order (**Pending**)
- Get Saved Orders by Path (**Pending**)
- Replace Saved Order (**Pending**)

### Accounts
- Get Account (Implemented)
- Get Accounts (Implemented)

## Instruments
- Search Instruments (Implemented - **NEW**)
- Get Instruments (Implemented - **NEW**)

## Market Hours
- Get Hours for Multiple Markets (Implemented)
- Get Hours for a Single Market (Implemented)
            
## Movers
- Get Movers (**Partial**)

## Option Chains
- Get Option Chain (Implemented)

## Price History
- Get Price History (Implemented)
         
## Quotes
- Get Quote (Implemented)
- Get Quotes (Implemented)

## Transaction History
- Get Transaction (Implemented)
- Get Transactions (Implemented)

## User Info & Preferences
- Get Preferences (Implemented)
- Get Streamer Subscription Keys (Implemented)
- Get User Principals (Implemented)
- Update Preferences (Implemented)

## Watchlist
- Create Watchlist (**Pending**)
- Delete Watchlist (**Pending**)
- Get Watchlist (**Pending**)
- Get Watchlists for Multiple Accounts (**Pending**)
- Get Watchlists for Single Account (**Pending**)
- Replace Watchlist (**Pending**)
- Update Watchlist (**Pending**)

# Streaming (https://developer.tdameritrade.com/content/streaming-data)

This is the list of the subscription/events exposed by the streamer client.

## ADMIN
- LOGIN (Implemented - **NEW**)
- LOGOUT (Implemented - **NEW**)
- QOS (Implemented - **NEW**)

## ACCT_ACTIVITY
- ACCT_ACTIVITY (Implemented - **NEW**)

## ACTIVES
- ACTIVES_NASDAQ (**NOT** implemented)
- ACTIVES_NYSE (**NOT** implemented)
- ACTIVES_OPTIONS (**NOT** implemented)
- ACTIVES_OTCBB (**NOT** implemented)

## CHART
- CHART_EQUITY (Implemented - **NEW**)
- CHART_FUTURES (Implemented - **NEW**)
- CHART_OPTIONS (Implemented - **NEW**)
- CHART_HISTORY_FUTURES (**NOT** implemented)

## LEVEL ONE
- QUOTE (Implemented - **NEW**)
- OPTION (Implemented - **NEW**)
- LEVELONE_FUTURES (Implemented - **NEW**)
- LEVELONE_FOREX (Implemented - **NEW**)
- LEVELONE_FUTURES_OPTIONS (Implemented - **NEW**)

## BOOK (Not official documentation)
- FUTURES_BOOK (Implemented - **NEW**)
- FOREX_BOOK (Implemented - **NEW**)
- FUTURES_OPTIONS_BOOK (Implemented - **NEW**)
- LISTED_BOOK (Implemented - **NEW**)
- NASDAQ_BOOK (Implemented - **NEW**)
- OPTIONS_BOOK (Implemented - **NEW**)

## NEWS (Not too much information about this)
- NEWS_HEADLINE (**NOT** implemented)
- NEWS_HEADLINELIST (**NOT** implemented)
- NEWS_STORY (**NOT** implemented)

## TIMESALE
- TIMESALE_EQUITY (Implemented - **NEW**)
- TIMESALE_FOREX (Implemented - **NEW**)
- TIMESALE_FUTURES (Implemented - **NEW**)
- TIMESALE_OPTIONS (Implemented - **NEW**)

# Getting started (API)
**TOKENS_FILE** is the file generated in the Configuration File Genetation (First Time) Section
**CONSUMER_KEY** is in the Keys tab in the TD Ameritrade app)
    
    var client = new TDClient(TOKENS_FILE, CONSUMER_KEY);
    
    // Example to retrieve the account information
    var accounts = client.GetAccounts(new[] { "positions", "orders" }).Result.Data;

Check the **TDAmeritradeAPI.Example** project for full example information

# Getting started (Streamer)

**TOKENS_FILE** is the file generated in the Configuration File Genetation (First Time) Section
**CONSUMER_KEY** is in the Keys tab in the TD Ameritrade app)

    
    var client = new TDClient(TOKENS_FILE, CONSUMER_KEY);
    var clientStreamer = new TDStreamClient(client);
    
    clientStreamer.OnLogin += (sender, data) =>
    {
        if (data.Code == 0)
        {
            // Enter your subscriptions here
        }
    }
    
    clientStreamer.Start();
    clientStreamer.Login();
    Console.ReadKey();
    clientStreamer.Stop();

Check the **TDAmeritradeAPI.ExampleStreaming** project for full example information.

# Attributions and Trademarks
The [original project](https://github.com/thes4s67/TDAmeritradeAPI) was developed and published by [thes4s67](https://github.com/thes4s67). Unfortunately, it appears to be abandoned :(
