Instructions for setting up and running SQL database and running it:

1. Install DockerHub
2. Open terminal from the DockerConfig root folder in project
3. In the terminal, run the following command to setup the SQL database as a docker container
    docker compose up --build
4. If you need to stop the docker container, run the following command (do not do this when running app as db will shut down)
    docker compose down
5. 
