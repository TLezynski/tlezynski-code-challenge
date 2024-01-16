# Thoughts and clarifications

### Task 1

Reading the description it was pretty clear the solution to Task 1 was a pretty straightforward recursive tree traversal with the added complexity of adhering to the proper architecture. I'm of the belief that the Controller and Repository objects should be of relative single responsibility, meaning they should simply handle their own requests. The business logic of determining extra information should be handled by the service, which is why I chose to implement it there.

The difficult part of this task didn't come from solving a recursive problem though, but rather debugging why the direct reports weren't being populated in my in-memory database. There's a comment in my code about it, in `EmployeeRepository::GetById`. I still don't understand why this is the case, but iterating over the collection once seems to initialize the list of direct reports, while otherwise it's null. I assumed this must've been an issue with my machine somehow, or some complexity that I have yet to understand about how the data is initialized in an in-memory database in .NET

### Task 2

In Task 2, I made the decision to use `effectiveDate` as a time from epoch String instead of a Date or DateTime object because I thought it would be easier to integrate with the given database in case there were any type mismatches, but this ended up being a pretty moot as it wasn't really used anywhere.

The part that really tripped me up was the use of the language "persist" and "persistence layer." I spent a quite a bit of time reading documentation about implementing caching, or writing the output to a file so it would persist between runtimes so it would add and write data to the seeded data. I finally took a step back, and I think I was barking up the wrong tree. I had already implemented it to be stored via the `EmployeeRepository`, a since that was beind treated as a database, I think the instructions were steering me towards using that instead of an in memory data structure.


### Tests

I was initially confused as to why the provided test cases were failing and eventually realized that the tests that were altering employee data were affecting the outcome of the `numberOfReports` value that was to be calculated by my endpoints. In an ideal scenario, each test should be able to run individually and clean up any altered data, or create/destroy its own data within its own run. Rather than try to alter the given tests, I decided the simplest solution for the sake of this exercise was to run my tests chronologically first in the test file. 

I also didn't write any long test cases where I created a large chain of employees to prove that the first task was solved adequately. I don't feel this is something that needed to be tested by a unit test, but was something I confirmed in my manual testing when creating the endpoint. I stuck with unit tests that checked a standard 200 route, and a 404 route.

-----------------

I hope I adequately addressed any concerns you might have had, and outline some of my thought process on decisions that I made.
