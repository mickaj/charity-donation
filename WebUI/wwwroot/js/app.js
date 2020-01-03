document.addEventListener("DOMContentLoaded", function () {

    /**
     * Form Select
     */
    class FormSelect {
        constructor($el) {
            this.$el = $el;
            this.options = [...$el.children];
            this.init();
        }

        init() {
            this.createElements();
            this.addEvents();
            this.$el.parentElement.removeChild(this.$el);
        }

        createElements() {
            // Input for value
            this.valueInput = document.createElement("input");
            this.valueInput.type = "text";
            this.valueInput.name = this.$el.name;

            // Dropdown container
            this.dropdown = document.createElement("div");
            this.dropdown.classList.add("dropdown");

            // List container
            this.ul = document.createElement("ul");

            // All list options
            this.options.forEach((el, i) => {
                const li = document.createElement("li");
                li.dataset.value = el.value;
                li.innerText = el.innerText;

                if (i === 0) {
                    // First clickable option
                    this.current = document.createElement("div");
                    this.current.innerText = el.innerText;
                    this.dropdown.appendChild(this.current);
                    this.valueInput.value = el.value;
                    li.classList.add("selected");
                }

                this.ul.appendChild(li);
            });

            this.dropdown.appendChild(this.ul);
            this.dropdown.appendChild(this.valueInput);
            this.$el.parentElement.appendChild(this.dropdown);
        }

        addEvents() {
            this.dropdown.addEventListener("click", e => {
                const target = e.target;
                this.dropdown.classList.toggle("selecting");

                // Save new value only when clicked on li
                if (target.tagName === "LI") {
                    this.valueInput.value = target.dataset.value;
                    this.current.innerText = target.innerText;
                }
            });
        }
    }
    document.querySelectorAll(".form-group--dropdown select").forEach(el => {
        new FormSelect(el);
    });

    /**
     * Hide elements when clicked on document
     */
    document.addEventListener("click", function (e) {
        const target = e.target;
        const tagName = target.tagName;

        if (target.classList.contains("dropdown")) return false;

        if (tagName === "LI" && target.parentElement.parentElement.classList.contains("dropdown")) {
            return false;
        }

        if (tagName === "DIV" && target.parentElement.classList.contains("dropdown")) {
            return false;
        }

        document.querySelectorAll(".form-group--dropdown .dropdown").forEach(el => {
            el.classList.remove("selecting");
        });
    });

    /**
     * Switching between form steps
     */
    class FormSteps {
        constructor(form) {
            this.$form = form;
            this.$next = form.querySelectorAll(".next-step");
            this.$prev = form.querySelectorAll(".prev-step");
            this.$step = form.querySelector(".form--steps-counter span");
            this.currentStep = 1;

            this.$stepInstructions = form.querySelectorAll(".form--steps-instructions p");
            const $stepForms = form.querySelectorAll("form > div");
            this.slides = [...this.$stepInstructions, ...$stepForms];

            this.init();
        }

        /**
         * Init all methods
         */
        init() {
            this.events();
            this.updateForm();
        }

        /**
         * All events that are happening in form
         */
        events() {
            // Next step
            this.$next.forEach(btn => {
                btn.addEventListener("click", e => {
                    e.preventDefault();

                    switch (this.currentStep) {
                        case 1: this.validateStepOne();
                            break;
                        case 2: this.validateStepTwo();
                            break;
                        case 3: this.validateStepThree();
                            break;
                        case 4: this.validateStepFour();
                            break;
                        default:
                            this.currentStep++;
                            this.updateForm();
                    }
                });
            });



            // Previous step
            this.$prev.forEach(btn => {
                btn.addEventListener("click", e => {
                    e.preventDefault();
                    this.currentStep--;
                    this.updateForm();
                });
            });

            // Form submit
            this.$form.querySelector("form").addEventListener("submit", e => this.submit(e));
        }

        validateStepOne() {
            var errorMessageElement = document.getElementById('category-error-message');
            var okStepOne = validateCheckboxGroup('categories-checkbox-group', 'category-item');
            if (okStepOne) {
                errorMessageElement.style.display = "none";
                this.currentStep++;
                this.updateForm();
            }
            else {
                errorMessageElement.style.display = "block";
            }
        }

        validateStepTwo() {
            var errorMessageElement = document.getElementById('bags-error-message');
            var numberOfBagsElement = document.getElementById('NumberOfBags');
            var actual = numberOfBagsElement.value;
            var max = numberOfBagsElement.getAttribute('max');
            var min = numberOfBagsElement.getAttribute('min');
            if (actual >= min && actual <= max) {
                errorMessageElement.style.display = "none";
                this.currentStep++;
                this.updateForm();
            }
            else {
                errorMessageElement.style.display = "block";
            }
        }

        validateStepThree() {
            var errorMessageElement = document.getElementById('institution-error-message');
            var okStepThree = validateCheckboxGroup('institution-checkbox-group', 'institution-item');
            if (okStepThree) {
                errorMessageElement.style.display = "none";
                this.currentStep++;
                this.updateForm();
            }
            else {
                errorMessageElement.style.display = "block";
            }
        }

        validateStepFour() {
            var errorMessageElement = document.getElementById('collection-error-message');
            var okStepFour = validateRequired(["CollectionData_Street", "CollectionData_City", "CollectionData_ZipCode", "CollectionData_PhoneNumber", "CollectionData_Date", "CollectionData_Time"]);
            if (okStepFour) {
                errorMessageElement.style.display = "none";
                this.currentStep++;
                this.updateForm();
                this.fillSummary();
            }
            else {
                errorMessageElement.style.display = "block";
            }
        }


        /**
       * Update form front-end
       * Show next or previous section etc.
       */
        updateForm() {
            this.$step.innerText = this.currentStep;

            this.slides.forEach(slide => {
                slide.classList.remove("active");

                if (slide.dataset.step == this.currentStep) {
                    slide.classList.add("active");
                }
            });

            this.$stepInstructions[0].parentElement.parentElement.hidden = this.currentStep >= 5;
            this.$step.parentElement.hidden = this.currentStep >= 5;
        }

        fillSummary() {
            var bagsCount = document.getElementById('NumberOfBags').value;
            var category = findPickedCategories("categories-checkbox-group", "category-item");
            var institution = findPickedCategories("institution-checkbox-group", "institution-item");

            var itemString = document.getElementById('summary-items-string').innerHTML.replace("%CATEGORY%", category).replace("% COUNT% ", bagsCount);
            var institutionString = document.getElementById('summary-institution-string').innerHTML.replace("%INSTITUTION%", institution);

            document.getElementById('summary-items').innerHTML = itemString;
            document.getElementById('summary-institution').innerHTML = institutionString;

            document.getElementById('summary-street').innerHTML = document.getElementById('CollectionData_Street').value;
            document.getElementById('summary-city').innerHTML = document.getElementById('CollectionData_City').value;
            document.getElementById('summary-zip').innerHTML = document.getElementById('CollectionData_ZipCode').value;
            document.getElementById('summary-phone').innerHTML = document.getElementById('CollectionData_PhoneNumber').value;

            document.getElementById('summary-date').innerHTML = document.getElementById('CollectionData_Date').value;
            document.getElementById('summary-time').innerHTML = document.getElementById('CollectionData_Time').value;

            var notes = document.getElementById('CollectionData_Notes').value;
            if (notes.length > 0) {
                document.getElementById('summary-notes').innerHTML(notes);
            }
        }
    }
    const form = document.querySelector(".form--steps");
    if (form !== null) {
        new FormSteps(form);
    }
});

function validateCheckboxGroup(groupId, itemClass) {
    var checkboxes = document.getElementById(groupId).getElementsByClassName(itemClass);
    var result = false;
    for (i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked === true) {
            result = true;
        }
    }
    return result;
}

function validateRequired(inputIds) {
    var result = true;
    for (i = 0; i < inputIds.length; i++) {
        var element = document.getElementById(inputIds[i]);
        if (element.value.length < 1) {
            return false;
        }
    }
    return result;
}

function findPickedCategories(groupId, itemClass, separator = ", ") {
    var checkboxes = document.getElementById(groupId).getElementsByClassName(itemClass);
    var result = "";
    for (i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked === true) {
            if (result.length > 0) {
                result += separator;
            }
            result += checkboxes[i].dataset.categoryName;
        }
    }
    return result;
}