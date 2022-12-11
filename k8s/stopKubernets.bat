kubectl delete deploy cointracker-api-coinlist-depl
kubectl delete services cointracker-api-coinlist-services-srv

kubectl delete deploy cointracker-bus-rabbitmq-depl
kubectl delete services cointracker-bus-rabbitmq-srv

kubectl delete deploy -n ingress-nginx ingress-nginx-controller
kubectl delete services -n ingress-nginx ingress-nginx-controller