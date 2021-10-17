FROM rabbitmq:3-management
COPY SpendingSummary.Rabbitmq/definitions.json /etc/rabbitmq/definitions/definitions.json
COPY SpendingSummary.Rabbitmq/rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
RUN rabbitmq-plugins enable rabbitmq_management