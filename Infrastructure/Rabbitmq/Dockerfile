FROM rabbitmq:3-management
COPY Infrastructure/Rabbitmq/definitions.json /etc/rabbitmq/definitions/definitions.json
COPY Infrastructure/Rabbitmq/rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
RUN rabbitmq-plugins enable rabbitmq_management