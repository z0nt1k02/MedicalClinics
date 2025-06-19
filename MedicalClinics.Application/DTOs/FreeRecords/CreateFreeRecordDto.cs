namespace MedicalClinics.Application.DTOs.FreeRecords;

/// <param name="dateTime">Дата и время записи. Должны быть переданы в формате "dd.MM.yyyy HH:mm" (например: "12.01.2025 18:00")</param>
///

public record CreateFreeRecordDto(string dateTime);