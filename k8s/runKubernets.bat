kubectl apply -f cointracker-depl.yaml
kubectl apply -f local-pvc.yaml
kubectl apply -f rabbitmq-depl.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.5.1/deploy/static/provider/cloud/deploy.yaml
kubectl apply -f ingress-srv.yaml