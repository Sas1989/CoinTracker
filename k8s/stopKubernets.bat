kubectl delete -f cointrackercoin-depL.yaml
kubectl delete -f cointrackerwallet-depL.yaml
kubectl delete -f rabbitmq-depl.yaml
kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.5.1/deploy/static/provider/cloud/deploy.yaml
kubectl delete -f ingress.yaml
kubectl delete -f local-pvc.yaml
