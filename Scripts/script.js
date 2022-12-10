var api;
with (location) {
    api = href + "api"; /*"http://kotobu-001-site1.atempurl.com/api"*/
}


const textArea = document.querySelector(".request-input");

textArea.value = "{\"Name\": \"Ghoorbaghe\",\n\"Author\" : \"Ali mohammadzade\",\n\"Categorys\" : \"Amuzeshi\"," +
    "\"CC\" : \"This is a test\",\n\"CcDescription\" : \"This is a test for that\",\n\"Cover\" : \"Cover link\",\n" +
    "\"Description\" : \"this is a description\",\n\"IgLink\" : \"test link\",\n\"MainLink\" : \"this is the main link\"," +
    "\n\"TwitterLink\" : \"This is a twiter link\"}";

    const getBook = () => {
    axios.get(api + "/book")
    .then(data => console.log(data.data))
}

const getOneBook = () => {
    axios.get(api + "/book/1")
    .then(data => console.log(data.data))
}

const createNewBook = () => {
    let value = JSON.parse(textArea.value);
    if (value === '') {
        alert("Please Enter something in text Area...");
    }
    else {

        axios.post(api + "/book",
                value
            ).then(
                (response) => {
                    alert(response.data.Data);
                    console.log(response);
                }
            )
            .then(
                axios.get(api + "/book")
                .then(data => console.log(data.data))
            )
            .catch(err => console.log(err))
    }
    }
