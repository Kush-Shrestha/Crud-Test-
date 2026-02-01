# Student-Semester-Subject Relationship API

## Database Relationships

```
Student (Many) ??? (One) Semester (One) ??? (Many) SemesterSubject (Many) ??? (One) Subject
```

### Relationship Explanation:
- **Student ? Semester**: Many students belong to one semester
- **Semester ? Subject**: Many-to-Many relationship through `SemesterSubject` junction table
- **Result**: When you query a student, you get their semester and all subjects in that semester

## API Endpoints

### 1. Get All Students (Basic Info)
```
GET /api/Student
```
**Response**: List of students with basic info (Id, Name, Semester_id)

### 2. Get Student by ID (Basic Info)
```
GET /api/Student/{id}
```
**Response**: Single student with basic info

### 3. ?? Get Student with Full Details (NEW!)
```
GET /api/Student/{id}/details
```
**Response**: Complete student information including:
- Student ID and Name
- Semester ID and Name
- All subjects enrolled in that semester (ID, Name, Description)

**Example Response**:
```json
{
  "studentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "studentName": "John Doe",
  "semesterId": "7ba85f64-5717-4562-b3fc-2c963f66afa6",
  "semesterName": "Fall 2024",
  "subjects": [
    {
      "subjectId": "1ca85f64-5717-4562-b3fc-2c963f66afa6",
      "subjectName": "Mathematics",
      "description": "Advanced Calculus"
    },
    {
      "subjectId": "2da85f64-5717-4562-b3fc-2c963f66afa6",
      "subjectName": "Physics",
      "description": "Quantum Mechanics"
    }
  ]
}
```

## How to Set Up Data

### Step 1: Create Semesters
```
POST /api/Semester
Body: {
  "name": "Fall 2024"
}
```

### Step 2: Create Subjects
```
POST /api/Subject
Body: {
  "name": "Mathematics",
  "description": "Advanced Calculus"
}
```

### Step 3: Link Subjects to Semester
```
POST /api/SemesterSubject
Body: {
  "semester_id": "{semester-guid}",
  "subject_id": "{subject-guid}"
}
```

### Step 4: Create Students
```
POST /api/Student
Body: {
  "name": "John Doe",
  "semester_id": "{semester-guid}"
}
```

### Step 5: Get Student Details
```
GET /api/Student/{student-id}/details
```

## Testing Example

1. Create a semester "Spring 2024"
2. Create subjects: "Math", "Science", "History"
3. Link all 3 subjects to "Spring 2024" semester using SemesterSubject
4. Create a student "Alice" in "Spring 2024" semester
5. Call GET `/api/Student/{alice-id}/details`
6. You'll get Alice's info + "Spring 2024" + all 3 subjects!

## Technical Notes

- Uses Entity Framework Core with `.Include()` for eager loading
- Navigation properties properly configured
- Returns 404 if student not found
- Returns "Unknown" for semester name if relationship is broken
