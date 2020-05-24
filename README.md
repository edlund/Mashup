
# Mashup

A job interview challenge.

## Summary

The task: Create a REST API that combines API results from MusicBrainz,
Wikidata​/​Wikipedia and Cover Art Archive.

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

Since we have many third-party REST APIs we depend on, we will practice
a form of "Graceful Degradation"; only the initial request to MusicBrainz
to retrieve basic Artist information must be successful. Every other
request can fail, if it does we will simply leave out the missing information.

We should always interact with the data in an as strongly typed manner
as possible. This will require more work initially, but it will allow us
to avoid many common pitfalls otherwise discovered at runtime and will
allow us to work faster when the PoC is later reworked into a functional
product (if it ever is).

The documentation of any REST API is essential for its usefulness. We will
use Swashbuckle and Swagger to auto-generate the documentation for us.

    https://localhost:5001/swagger/index.html

## Current Problems

Things that are inferior or missing due to time constraints.

### Documentation

More documentation would allow Swagger to shine.

### Error handling

The error handling is servicable for a PoC, but nothing to write home about.
For example; the InvalidModelStateResponseFactory is set up but instead of
proper view model validation, a hack using a ValidationException is used.

### Local caching

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
Relational databases.)

Given enough time the local caching could be taken to its next level as
well; we could try to profile user behaviour to preemptively cache artists
we suspect will be requested in the future (a simple form could work with
genres and related artists, a complex form could work with machine learning
(which would be fun, but not necessary worth the time/money)). We could
obviously also crawl our consumed REST APIs and cache as much as possible
on our side, but we don't actually want to store more than necessary, even
though storage is pretty cheap nowadays.

### Logging

Only basic logging is configured, ideally we would use a third party
library and logging service, e.g. NLog (library) and PaperTrail (service)
would probably be a nice combo in our case.

### More Tests

Currently the automatic tests are almost exclusively blackbox/integration
tests where we make a request to a test server and check what it might
respond with.

In my personal opinion, this is fine for a PoC; we will likely rework
significant parts of our solution when we start leaving the "PoC phase",
and the most useful kind of test then is likely seeing that the overall
behaviour stays the same, regardless of what we do behind the scenes.
Unit tests can be significantly fleshed out once the design starts maturing.
With that excuse out of the way; more unit tests never hurts...

One obvious problem with this approach is that the test coverage will
be terrible in reports, even though it actually looks okay when we look
what is actually being tested by the blackbox tests. Another problem with
this approach is that we write our tests using data from third party sources,
which might change, causing test failures on our side. We can mitigate this
by writing tests that cares only about data validation (not the actual data)
or by selecting test data that is very unlikely to change. We are currently
using a mix of these two for the PoC. The data model should allow easy
mocking of data sources for future unit tests.

### Sloppy data modeling

The data models are not always complete and are sometimes only able to
handle the certain, exact responses from the REST APIs. For our limited
use cases in this PoC, this should be acceptable. However, the models
should be revisited and reworked (at least a bit), before more features
are implemented.

### Sloppy interface modeling

The data interfaces should probably be revisited at the same time that
the data models are. Fleshed out models will likely require some changes
to how interaction with them happens.

### Support for cloud deploys

The first step would be to evaluate which platform to use. Any of the big
ones available could be a good fit. Once a platform had been selected, the
next step would be to set up CI/CD and to tweak the automatic scaling.

The Dockerfile is a good start for the actual deployment, but it was written
in a haste and should probably be reworked a little before used in production.
HC SVNT DRACONES and all that...

## Next Steps

If I were to take this "to the next level", where would I begin?

Certainly error handling, local caching and logging.

## Overview

The solution is organized as follows:

- Mashup.Api: The REST API, i.e. Api+Mvc controllers
- Mashup.Core: The core functionality central to Mashup
- Mashup.Domain: The data model and the interfaces for interaction with it
- Mashup.Infrastructure: The implementations of the data model interfaces
- Mashup.Test: Unit, Integration and Blackbox tests

## Running

### Docker

First, make sure you have access to an Ubuntu 18.04 LTS instance. A virtual
machine is likely a good choice (VirtualBox is my go-to for development).

Start by setting up Docker and docker-compose, you can either follow the
official guides, or try to run `DockerSetup-Ubuntu1804.sh`, which is just
the guides line-by-line in a bash script. If you choose the latter, you
should manually verify the fingerprint output of the script.

Tip: Make sure to checkout the solution from Ubuntu to make sure that the
line-endings are Unix to save headache.

Once this is done:

    /path/to/project/root$ sudo docker-compose up

If we had to release this NOW, we could easily do so by using NGINX as a
reverse proxy for our application. Since we wouldn't have horizontal scaling
then, we would likely have to try to tweak Kestrel's worker threads to try to
handle high loads the best we could.

### Visual Studio

It should be possible to import the solution to Visual Studio 2019 and run
it from there (which is how it was developed), provided that the appropriate
packages are installed. But Docker is the recommended method.

## Closing Notes

I really, really miss ReSharper as a companion during my C# adventures. I
currently don't write enough C# in my spare time to justify the license cost
to myself.
