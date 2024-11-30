// JavaScript to dynamically update the TargetDate
function updateTargetDate() {
    // Get the selected workout
    const workoutSelect = document.getElementById("workOutId");
    const selectedOption = workoutSelect.options[workoutSelect.selectedIndex];

    // Extract the target date from the selected option's data attribute
    const targetDate = selectedOption.getAttribute("data-target-date");

    // Set the TargetDate input value
    document.getElementById("targetDate").value = targetDate || "";
}