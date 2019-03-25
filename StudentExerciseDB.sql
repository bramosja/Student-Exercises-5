DROP TABLE IF EXISTS StudentExercise;
DROP TABLE IF EXISTS Student;
DROP TABLE IF EXISTS Instructor;
DROP TABLE IF EXISTS Exercise;
DROP TABLE IF EXISTS Cohort;

CREATE TABLE Cohort (
	Id INTEGER Not Null PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) Not Null
);

CREATE TABLE Exercise (
	Id INTEGER Not Null PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) Not Null,
	[Language] VARCHAR(50) Not Null
);

CREATE TABLE Instructor (
	Id INTEGER Not Null PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) Not Null,
	LastName VARCHAR(50) Not Null,
	SlackHandle VARCHAR(50) Not Null,
	CohortId INTEGER Not Null,
	CONSTRAINT FK_InstructorCohort FOREIGN KEY(CohortId) REFERENCES Cohort(Id)
);

CREATE TABLE Student (
	Id INTEGER Not Null PRIMARY KEY IDENTITY,
	firstName VARCHAR(50) Not Null,
	lastName VARCHAR(50) Not Null,
	SlackHandle VARCHAR(50) Not Null,
	CohortId INTEGER Not Null,
	CONSTRAINT FK_StudentCohort FOREIGN KEY(CohortId) REFERENCES Cohort(Id)
);

CREATE TABLE StudentExercise (
	Id INTEGER Not Null PRIMARY KEY IDENTITY,
	StudentId INTEGER Not Null,
	ExerciseId INTEGER Not Null,
	CONSTRAINT FK_StudentExerciseStudent FOREIGN KEY (StudentId) REFERENCES Student(Id),
	CONSTRAINT FK_ExerciseIdExercise FOREIGN KEY (ExerciseId) REFERENCES Exercise(Id)
);

INSERT INTO Cohort ([Name]) VALUES ('c28');
INSERT INTO Cohort ([Name]) VALUES ('c29');
INSERT INTO Cohort ([Name]) VALUES ('c30');
INSERT INTO Cohort ([Name]) VALUES ('c31');

INSERT INTO Exercise ([Name], [Language])
	VALUES ('Comments', 'JavaScript');

INSERT INTO Exercise ([Name], [Language])
	VALUES ('Methods', 'C#');

INSERT INTO Exercise ([Name], [Language])
	VALUES ('Arrays', 'JavaScript');

INSERT INTO Exercise ([Name], [Language])
	VALUES ('Donuts', 'C#');

INSERT INTO Exercise ([Name], [Language])
	VALUES ('Spaghetti', 'Python');


INSERT INTO Student (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Elyse', 'Dawson', '@bestbigsib', 1);

INSERT INTO Student (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Brittany', 'Ramos-Janeway', '@bramosja', 2),
		   ('Cole', 'Slaw', '@theColeSlaw', 2),
		   ('Austin', 'Blade', '@BladezOfMaury', 2);

INSERT INTO Student (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Brian', 'HannahsHubby', '@HannahistheBest', 3);

INSERT INTO Student (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Abbey', 'Brown', '@notreallyc31', 4),
			('Jameka', 'Echols', '@actuallyInC31', 4);



INSERT INTO Instructor (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Joe', 'Shep', '@CuppaJoe', 1);

INSERT INTO Instructor (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Andy', 'Collins', '@CollinMeMaybe', 2);

INSERT INTO Instructor (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Steve', 'Brownlee', '@ColorMeBrownlee', 3);

INSERT INTO Instructor (firstName, lastName, SlackHandle, CohortId)
	VALUES ('Jisie', 'David', '@c29IsMyFavoriteCohort', 4);


INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (1, 5);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (2, 2);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (3, 2);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (4, 2);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (2, 4);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (3, 4);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (4, 4);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (2, 3);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (5, 1);

INSERT INTO StudentExercise (StudentId, ExerciseId)
	VALUES (5, 3),
			(6, 3),
			(7, 3);