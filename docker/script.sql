IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [CourseCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CoverImage] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CourseCategories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [Country] int NOT NULL,
    [Age] int NOT NULL,
    [Gender] int NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Teachers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [Country] int NOT NULL,
    [Age] int NOT NULL,
    [Gender] int NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [CourseCategoryId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Language] nvarchar(max) NOT NULL,
    [RateToPass] int NOT NULL,
    [TimeToPass] int NOT NULL,
    [CoverImage] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_CourseCategories_CourseCategoryId] FOREIGN KEY ([CourseCategoryId]) REFERENCES [CourseCategories] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AnswerAssignments] (
    [Id] int NOT NULL IDENTITY,
    [LastTrial] datetime2 NOT NULL,
    [AttemptsTaken] int NOT NULL,
    [AssignmentId] int NOT NULL,
    [StudentId] int NOT NULL,
    CONSTRAINT [PK_AnswerAssignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnswerAssignments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [CourseTeachers] (
    [Id] int NOT NULL IDENTITY,
    [TeacherId] int NOT NULL,
    [CourseId] int NOT NULL,
    CONSTRAINT [PK_CourseTeachers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CourseTeachers_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseTeachers_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Enrollments] (
    [StudentId] int NOT NULL,
    [CourseId] int NOT NULL,
    [Id] int NOT NULL,
    [EnrollDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY ([CourseId], [StudentId]),
    CONSTRAINT [FK_Enrollments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Modules] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CourseId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [StudentId] int NOT NULL,
    [CourseId] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Rank] int NOT NULL,
    [DateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Assignments] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ModuleId] int NOT NULL,
    [Order] int NOT NULL,
    [Duration] int NOT NULL,
    [RateToPass] int NOT NULL,
    [MaxTrials] int NOT NULL,
    CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Assignments_Modules_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [Modules] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Lessons] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ModuleId] int NOT NULL,
    [Order] int NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lessons_Modules_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [Modules] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Questions] (
    [Id] int NOT NULL IDENTITY,
    [CreationDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [MaxGrade] int NOT NULL,
    [AssignmentId] int NOT NULL,
    [IsMultipleChoice] bit NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Questions_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Thoughts] (
    [Id] int NOT NULL IDENTITY,
    [CreationDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [MaxGrade] int NOT NULL,
    [AssignmentId] int NOT NULL,
    CONSTRAINT [PK_Thoughts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Thoughts_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [QuestionOptions] (
    [Id] int NOT NULL IDENTITY,
    [QuestionId] int NOT NULL,
    [Text] nvarchar(max) NULL,
    [Correct] bit NOT NULL,
    CONSTRAINT [PK_QuestionOptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_QuestionOptions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AnswerThoughts] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [Grade] int NOT NULL,
    [ThoughtId] int NOT NULL,
    [AnswerAssignmentId] int NOT NULL,
    CONSTRAINT [PK_AnswerThoughts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnswerThoughts_AnswerAssignments_AnswerAssignmentId] FOREIGN KEY ([AnswerAssignmentId]) REFERENCES [AnswerAssignments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnswerThoughts_Thoughts_ThoughtId] FOREIGN KEY ([ThoughtId]) REFERENCES [Thoughts] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AnswerQuestionOptions] (
    [Id] int NOT NULL IDENTITY,
    [Correct] bit NOT NULL,
    [QuestionOptionId] int NOT NULL,
    [AnswerAssignmentId] int NOT NULL,
    CONSTRAINT [PK_AnswerQuestionOptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnswerQuestionOptions_AnswerAssignments_AnswerAssignmentId] FOREIGN KEY ([AnswerAssignmentId]) REFERENCES [AnswerAssignments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnswerQuestionOptions_QuestionOptions_QuestionOptionId] FOREIGN KEY ([QuestionOptionId]) REFERENCES [QuestionOptions] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AnswerAssignments_StudentId] ON [AnswerAssignments] ([StudentId]);

GO

CREATE INDEX [IX_AnswerQuestionOptions_AnswerAssignmentId] ON [AnswerQuestionOptions] ([AnswerAssignmentId]);

GO

CREATE INDEX [IX_AnswerQuestionOptions_QuestionOptionId] ON [AnswerQuestionOptions] ([QuestionOptionId]);

GO

CREATE INDEX [IX_AnswerThoughts_AnswerAssignmentId] ON [AnswerThoughts] ([AnswerAssignmentId]);

GO

CREATE INDEX [IX_AnswerThoughts_ThoughtId] ON [AnswerThoughts] ([ThoughtId]);

GO

CREATE INDEX [IX_Assignments_ModuleId] ON [Assignments] ([ModuleId]);

GO

CREATE INDEX [IX_Courses_CourseCategoryId] ON [Courses] ([CourseCategoryId]);

GO

CREATE INDEX [IX_CourseTeachers_CourseId] ON [CourseTeachers] ([CourseId]);

GO

CREATE INDEX [IX_CourseTeachers_TeacherId] ON [CourseTeachers] ([TeacherId]);

GO

CREATE INDEX [IX_Enrollments_StudentId] ON [Enrollments] ([StudentId]);

GO

CREATE INDEX [IX_Lessons_ModuleId] ON [Lessons] ([ModuleId]);

GO

CREATE INDEX [IX_Modules_CourseId] ON [Modules] ([CourseId]);

GO

CREATE INDEX [IX_QuestionOptions_QuestionId] ON [QuestionOptions] ([QuestionId]);

GO

CREATE INDEX [IX_Questions_AssignmentId] ON [Questions] ([AssignmentId]);

GO

CREATE INDEX [IX_Reviews_CourseId] ON [Reviews] ([CourseId]);

GO

CREATE INDEX [IX_Reviews_StudentId] ON [Reviews] ([StudentId]);

GO

CREATE INDEX [IX_Thoughts_AssignmentId] ON [Thoughts] ([AssignmentId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201130075401_newmigration', N'3.1.8');

GO

ALTER TABLE [Students] ADD [ProfileImage] nvarchar(max) NOT NULL DEFAULT N'';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201214085539_AddedProfilePic', N'3.1.8');

GO


