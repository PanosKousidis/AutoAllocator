# Auto allocator

## Summary

This console application is designed for the purpose of allocating students to supervisors, 
based on topic interest and supervisor availability.

## Logic

The application starts by simply executing it, in which case it will ask for the paths of 
the csv files that contain the students and the supervisors. Alternatively, these files can
be passed as arguments when launching it (first supervisors, second students):
```
AutoAllocator.exe c:\temp\supervisors.csv c:\temp\students.csv
```

In the input files, the following are assigned:
* topics of interest for each student 
* topics for each supervisor
* the capacity of each supervisor (how many students they can supervise)

When run, the application will parse the input files and apply the following logic to assign the supervisors:
- Loop through the students and find the supervisor with the most common interests
- The supervisor needs to have capacity  > 0
- When there is a tie, (e.g. 5 supervisors found with 1 common interest), select the supervisor with the lowest utilization (see below)
- Once a supervisor is selected, 
  - set the utilisation of the supervisor to {slotsTaken}/{capacity}, so on a next tie, another one will be selected with less utilisation
  - reduce the supervisor's capacity by 1

## Input

### Students

The input of the csv file is expected to be in the following format:

```
Name,TOPIC A,TOPIC B,TOPIC C,TOPIC D,TOPIC E,TOPIC F
STUDENT 1,,X,,X,,X
STUDENT 2,,X,,X,,X
STUDENT 3,X,,X,,,
STUDENT 4,,,,X,,
STUDENT 5,,,,,X,
```
This is visually represented as:

| Name      | TOPIC A | TOPIC B | TOPIC C | TOPIC D | TOPIC E | TOPIC F |
|-----------|---------|---------|---------|---------|---------|---------|
| STUDENT 1 |         | X       |         | X       |         | X       |
| STUDENT 2 |         | X       |         | X       |         | X       |
| STUDENT 3 | X       |         | X       |         |         |         |
| STUDENT 4 |         |         |         | X       |         |         |
| STUDENT 5 |         |         |         |         | X       |         |

### Supervisors

The input of the supervisors csv is expected to be in the following format:

```
Name,Capacity,TOPIC A,TOPIC B,TOPIC C,TOPIC D,TOPIC E,TOPIC F
SUPERVISOR 1,15,,X,,X,,X
SUPERVISOR 2,15,X,X,,X,,X
SUPERVISOR 3,15,,X,X,,X,
SUPERVISOR 4,15,X,X,X,,,
SUPERVISOR 5,15,,,X,X,X,X
```

This is visually represented as:

| Name         | Capacity | TOPIC A | TOPIC B | TOPIC C | TOPIC D | TOPIC E | TOPIC F |
|--------------|----------|---------|---------|---------|---------|---------|---------|
| SUPERVISOR 1 | 15       |         | X       |         | X       |         | X       |
| SUPERVISOR 2 | 15       | X       | X       |         | X       |         | X       |
| SUPERVISOR 3 | 15       |         | X       | X       |         | X       |         |
| SUPERVISOR 4 | 15       | X       | X       | X       |         |         |         |
| SUPERVISOR 5 | 15       |         |         | X       | X       | X       | X       |

## Output

The output is currently just console lines that resemble the following format:
```
SUPERVISOR 1 (1/15 (TOPIC B,TOPIC D,TOPIC F)
        STUDENT 1 (TOPIC B,TOPIC D,TOPIC F)

SUPERVISOR 2 (1/15 (TOPIC A,TOPIC B,TOPIC D,TOPIC F)
        STUDENT 2 (TOPIC B,TOPIC D,TOPIC F)

SUPERVISOR 3 (1/15 (TOPIC B,TOPIC C,TOPIC E)
        STUDENT 5 (TOPIC E)
 
 SUPERVISOR 4 (1/15 (TOPIC A,TOPIC B,TOPIC C)
        STUDENT 3 (TOPIC A,TOPIC C) 
 
 SUPERVISOR 5 (1/15 (TOPIC C,TOPIC D,TOPIC E,TOPIC F)
        STUDENT 4 (TOPIC D)     
```
(for a more complete example check the sample files in the Resources directory)