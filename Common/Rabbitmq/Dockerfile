FROM rabbitmq:3-management
COPY Common/Rabbitmq/definitions.json /etc/rabbitmq/definitions/definitions.json
COPY Common/Rabbitmq/rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
RUN rabbitmq-plugins enable rabbitmq_management