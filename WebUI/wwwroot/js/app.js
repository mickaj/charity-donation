document.addEventListener("DOMContentLoaded", function() {

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
  document.addEventListener("click", function(e) {
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
            if (this.currentStep === 1) {
                var okStepOne = validateCheckboxGroup('categories-checkbox-group', 'category-item');
                if (okStepOne) {
                    $('#category-error-message').addClass('d-none');
                    this.currentStep++;
                    this.updateForm();
                }
                else {
                    //alert("form validity: " + okToMoveOn);
                    $('#category-error-message').removeClass('d-none');
                }
            }
            else if (this.currentStep === 2) {
                var numberOfBags = $('#NumberOfBags').val();
                if (numberOfBags > 0) {
                    $('#bags-error-message').addClass('d-none');
                    this.currentStep++;
                    this.updateForm();
                }
                else {
                    $('#bags-error-message').removeClass('d-none');
                }
            }
            else if (this.currentStep === 3) {
                var okStepThree = validateCheckboxGroup('institution-checkbox-group', 'institution-item');
                if (okStepThree) {
                    $('#institution-error-message').addClass('d-none');
                    this.currentStep++;
                    this.updateForm();
                }
                else {
                    //alert("form validity: " + okStepTwo);
                    $('#institution-error-message').removeClass('d-none');
                }
            }
            else if (this.currentStep === 4) {
                var okStepFour = validateRequired(["CollectionData_Street", "CollectionData_City", "CollectionData_ZipCode", "CollectionData_PhoneNumber", "CollectionData_Date", "CollectionData_Time"]);
                if (okStepFour) {
                    $('#collection-error-message').addClass('d-none');
                    this.currentStep++;
                    this.updateForm();
                    fillSummary();
                }
                else {
                    //alert("form validity: " + okStepTwo);
                    $('#collection-error-message').removeClass('d-none');
                }
            }
            else {
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

    /**
     * Update form front-end
     * Show next or previous section etc.
     */
    updateForm() {
      this.$step.innerText = this.currentStep;

      // TODO: Validation

      this.slides.forEach(slide => {
        slide.classList.remove("active");

        if (slide.dataset.step == this.currentStep) {
          slide.classList.add("active");
        }
      });

      this.$stepInstructions[0].parentElement.parentElement.hidden = this.currentStep >= 5;
      this.$step.parentElement.hidden = this.currentStep >= 5;

      // TODO: get data from inputs and show them in summary
    }

  }
  const form = document.querySelector(".form--steps");
  if (form !== null) {
    new FormSteps(form);
  }
});

function validateCheckboxGroup(groupId, itemClass) {
    var checkboxes = $('#' + groupId).find('.' + itemClass);
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
        if ($("#" + inputIds[i]).val().length < 1) {
            return false;
        }
    }
    return result;
}

function fillSummary() {
    var bagsCount = $("#NumberOfBags").val();
    var category = findPickedCategories("categories-checkbox-group", "category-item");
    var institution = findPickedCategories("institution-checkbox-group", "institution-item");

    var itemString = $("#summary-items-string").html().replace("%CATEGORY%", category).replace("%COUNT%", bagsCount);

    var institutionString = $("#summary-institution-string").html().replace("%INSTITUTION%", institution);

    $("#summary-items").html(itemString);
    $("#summary-institution").html(institutionString);

    $("#summary-street").html($("#CollectionData_Street").val());
    $("#summary-city").html($("#CollectionData_City").val());
    $("#summary-zip").html($("#CollectionData_ZipCode").val());
    $("#summary-phone").html($("#CollectionData_PhoneNumber").val());

    //var date = new Date($("#CollectionData_Date").val() + "T" + $("#CollectionData_Time").val());
    //var dateString = date.getDay() + "/" +date.getMonth()+"/"+date.getFullYear();
    //var timeString = date.getHours()+":"+date.getMinutes();

    $("#summary-date").html($("#CollectionData_Date").val());
    $("#summary-time").html($("#CollectionData_Time").val());

    var notes = $("#CollectionData_Notes").val();
    if (notes.length > 0) {
        $("#summary-notes").html();
    }    
}

function findPickedCategories(groupId, itemClass) {
    var checkboxes = $('#' + groupId).find('.' + itemClass);
    var result = "";
    for (i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked === true) {
            if (result.length > 0) {
                result += ", ";
            }
            result += checkboxes[i].dataset.categoryName;
        }
    }
    return result;
}
