
# Mashup

A job interview challenge.

## Summary

The task: Create a REST API that combines API results from MusicBrainz,
Wikidata​/​Wikipedia and Cover Art Archive​.

The challenge: The APIs that are to be consumed are sometimes on the
slow side and will impose rate limiting under heavy loads.

The requirements:

- Create a PoC REST API capable of:
  - Given a MBID, return a response consisting of:
    - A description of the artist from Wikipedia
    - A list of all albums released by the requested artist, with links
      for each album to its Cover Art image
  - Running under a heavy load
- Make the PoC deployable

## Solution

To deal with heavy loads better, the REST API should be possible to scale
horizontally/out. Vertical scaling will only work to a point for a REST
API that is heavily dependent on HTTP(S) communication with other REST APIs.
Under heavy loads we will likely start seeing socket exhaustion causing
long response times and even timeouts for requests to us.

Horizontal scaling can address this if we run multiple instances on different
VPSs since each VPS will have its own socket pool to work with.

We should select a hosting platform that can provide automatic horizontal
scaling based on user selectable criterias, e.g.; AWS, Azure, Heroku, etc..

If we run all requests to our consumed REST APIs sequentially we will likely
see long response times and long request queues. We should therefore try to
run requests against our consumed REST APIs asynchronously and in parallel
when possible. This is handled with async/await.

Local caching of the result of requests to our REST API allows us to serve
subsequent requests for the same MBID (much) faster. The question is of course
what client behaviour we can expect; a local cache isn't worth that much
if we rarely get requests for the same artists within a reasonable timeframe.

The cache could be implemented using a key/value store (e.g. Redis), a
document database (e.g. MongoDb) or a relational database (e.g. Postgres).
For this PoC, MongoDb would be a good choice. The data model is pretty simple,
but we could use a proper, persistent database to store data caches and user
behaviour. Had the data model been more complex, Postgres would likely have
been a better choice. (Though it should be noted that Document and Relational
databases are meeting in the middle when it comes to features; Joins and
Transactions for Document databases and Array and Unstructured fields for
Relational databases.) This will NOT be done as part of the PoC.

Given enough time the local caching could be taken to its next level as
well; we could try to profile user behaviour to preemptively cache artists
we suspect will be requested in the future (a simple form could work with
genres and related artists). We could obviously also crawl and cache as
much as possible on our side, but we don't actually want to store more
than necessary, even though storage is pretty cheap. This will NOT be
done as part of the PoC.

## Overview

The solution is organized as follows:

- Mashup.Api: The REST API
- Mashup.Core: The core functionality that most projects depend on
- Mashup.Domain: The data model and the interfaces for interaction with it
- Mashup.Infrastructure: The implementations of the data model interfaces
- Mashup.Test: Unit, Integration and Blackbox tests
