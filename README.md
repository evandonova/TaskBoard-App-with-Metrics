# TaskBoard App with Metrics
Modified the "**Task Board**" ASP.NET Core 6 app (https://github.com/nakov/TaskBoard) to **export metrics** and report them to a summary, histogram, gauge and counter, using **middleware**.

#### Metrics are available on `/metrics`:
<kbd>
  <img src="https://github.com/evandonova/TaskBoard-App-with-Metrics/assets/69080997/e9603a71-1110-4dea-9f94-8c2bee221a11" width="700" height="500" />
</kbd>

#### The app exports the following metrics:
- **Default app metrics** related to HTTP requests and application performance
- **App requests counter**
  <kbd>
    <img src="https://github.com/evandonova/TaskBoard-App-with-Metrics/assets/69080997/306d8c9d-6697-480d-9de0-5dea7e90d80a" width="500" height="130" />
  </kbd>

- **Requests duration summary**
  <kbd>
    <img src="https://github.com/evandonova/TaskBoard-App-with-Metrics/assets/69080997/33fa48c5-7100-4ae7-80aa-6410e45cb39f" width="400" height="55" />
  </kbd>

- **Responses size histogram**
  <kbd>
    <img src="https://github.com/evandonova/TaskBoard-App-with-Metrics/assets/69080997/f2f365b6-3c15-4293-ba74-0eee2a254e8f" width="300" height="200" />
  </kbd>

- **Tasks in boards gauge**
  <kbd>
    <img src="https://github.com/evandonova/TaskBoard-App-with-Metrics/assets/69080997/d68b203e-75e7-459c-abdc-0b93b802e0fe" width="500" height="70" />
  </kbd>


These metrics can be used to **monitor and visualize app data** with Prometheus + Grafana or another observability stack.


