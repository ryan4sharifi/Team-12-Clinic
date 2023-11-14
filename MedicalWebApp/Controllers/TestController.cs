@model IEnumerable<MedicalWebApp.Models.Appointment>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.DateAppointment)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.OfficeId)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Doctor)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Patient)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.DateAppointment)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.OfficeId)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Doctor.DoctorId)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Patient.PatientId)
            </td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.AppointmentId">Edit</a> |
                <a asp-action="Details" asp-route-id="@item.AppointmentId">Details</a> |
                <a asp-action="Delete" asp-route-id="@item.AppointmentId">Delete</a>
            </td>
        </tr>
}
    </tbody>
</table>
