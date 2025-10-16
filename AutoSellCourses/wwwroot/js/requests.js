const clientcars_uri = "api/clientcars";
const client_uri = "api/client";
const contracts_uri = "api/contract";

let clientcars = [];
let clients = [];

function getClientCars() {
  fetch(clientcars_uri)
    .then((response) => response.json())
    .then((data) => _displayClientCars(data))
    .catch((error) => console.error("Не можем вывести данные карточек", error));

}

function getStats() {
    fetch(`/api/contract/stats`)
        .then((response) => response.json())
        .then((data) => _displayStats(data))
        .catch((error)=> console.error("Не можем вывести статистику", error))
    }
    
function _displayStats(data) {
    const statsFrame = document.getElementById('stats-content');
    console.log(data);

    statsFrame.innerHTML = ``;


    if (!data || data.length == 0) {

        statsFrame.innerHTML = `<p>Нету вывода статистики!</p>`
        return

    } 
    const statsConfig = [

        { key: 'totalContracts', title: 'Всего контрактов:'},
        { key: 'todayContracts', title: 'Контрактов сегодня:'},
        { key: 'thisMonthContracts', title: 'Контрактов в этом месяце:'}

    ];

    statsConfig.forEach((stat) => {
        statsFrame.innerHTML += `
        <div class="stats-info">
                <div class="stats-info-title">
                  ${stat.title}
                </div>
                <div class="stats-info-count">
                  ${data[stat.key] || 0}
                </div>
              </div>
        `;
    });
};


//function getClients() {

//    fetch(client_uri)
//        .then(response => response.json())
//        .then(data => _displayClientCars())

//}



function _displayClientCars(data) {
  const cardFrame = document.getElementById("frame-cards");
  console.log(data);
  cardFrame.innerHTML = ``;

  if (!data || data.length === 0) {
    cardFrame.innerHTML = `<div class="noResult">Нет доступных машин!</div>`;
    return;
  }

  data.forEach((clientcar) => {
    cardFrame.innerHTML += `
        <div class="car-card">
              <div class="picture-car"></div>
              <div class="car-name">${clientcar.carBrand}, ${clientcar.manufactureDate}, ${clientcar.carsColor}</div>
              <div class="owner-car">
                ${clientcar.client}: ${clientcar.clientNumber}
              </div>
              <div class="car-characteristics"> 
                <div class="mileage-car">
                  <img
                    class="icon-car-characterictics"
                    src="img/tachometer(1).png"
                    alt=""
                  />
                  ${clientcar.mileage}
                </div>
                <div class="car-transmission-type">
                  <img
                    class="icon-car-characterictics"
                    src="img/drivetrain(1).png"
                    alt=""
                  />
                  ${clientcar.transmissionType}
                </div>
                <div class="car-engine-capacity">
                  <img
                    class="icon-car-characterictics"
                    src="img/car-engine(1).png"
                    alt=""
                  />
                  ${clientcar.engineCapacity}
                </div>
              </div>

              <div class="car-desc-price">
                  <div class="car-description">
                      ${clientcar.description}
                    </div>
                    <div class="car-price">
                    ${clientcar.price} 
                    </div>
            </div>
              
              <div class="card-btns">
                <div class="sell-btn-card">
                  <button onClick = "buyCar(${clientcar.id})">Купить авто</button>
                </div>
                <div class="credit-btn-card">
                  <button>Оформить кредит</button>
                </div>
              </div>
        `;
  });
  clientcars = data;
}

async function buyCar(carId) {
  //const today = new Date();
  //const contractDate = today.toISOString().split('T')[0];
  //const contractData = {
  //    Contract: {
  //        ContractDate: contractDate
  //    }
  //}
  try {
    const response = await fetch(`/${contracts_uri}/${carId}`, {
      method: "POST",
      //headers: {
      //    "Content-type": "application/json"
      //},
      //body: JSON.stringify(contractData)
    });

    if (!response) {
      throw new Error(`Проблема с запросом: ${response.status}`);
    }
    const result = await response.json();
      getClientCars();
      getStats();

    return result;
  } catch (error) {
    console.error(`Ошибка: ${error}`);
  }
}

async function addClientWithCar() {
  const fullName = document.getElementById("fio").value;

  const partsName = fullName.trim().split(/\s+/);

  const year = parseInt(document.getElementById("car-year").value);

  const ManufactureDate = new Date(year, 0, 1);

  const formData = {
    Client: {
      ClientLastName: partsName[0],
      ClientName: partsName[1],
      ClientMiddleName: partsName[2],
      ClientTown: document.getElementById("city").value,
      ClientAddress: document.getElementById("address").value,
      ClientNumber: document.getElementById("number-phone").value,
    },
    ClientCar: {
      CarBrand: document.getElementById("car-name").value,
      Mileage: parseInt(document.getElementById("car-mileage").value),
      ManufactureDate: ManufactureDate,
      Description: document.getElementById("car-description").value,
      ColorCar: document.getElementById("car-color").value,
      TransmissionType: document.getElementById("transmission-type").value,
      EngineCapacity: document.getElementById("car-power").value,
      Price: parseFloat(document.getElementById("car-price").value),
    },
  };
  try {
    const response = await fetch(`/api/clientcars/with-clients`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
        .then(() => getClientCars())
        .then(() => popUpContainer.classList.remove('active'))
      .catch((error) =>
        console.error(
          "Не смогли зарегестрировать пользователя и автомобиль",
          error
        )
      );

    if (!response.ok) {
      throw new Error(`Ошибка сервера: ${response.status}`);
    }

    const result = await response.json();
    return result;
  } catch (error) {
    console.log("Ошибка: ", error);
  }
}


