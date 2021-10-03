# Learning k8s 

## k8s dashboard
`microk8s dashboard-proxy`

**OR**

`microk8s kubectl port-forward -n kube-system service/kubernetes-dashboard 10443:443`

https://127.0.0.1:10443

## Add microk8s' kubectl as alias
`alias kubectl='microk8s kubectl'`

## Add microk8s' kubectl as binary
`sudo snap alias microk8s.kubectl kubectl`

Revert with `snap unalias`

## Update cluster resources using YAML

### Create & Update: `kubectl apply -f <resource_file_name>`
### Diff: `kubectl diff -f <resource_file_name>`
### Delete: `kubectl delete -f <resource_file_name>`
### Apply with Kustomize: `kubectl apply -k <kustomization_dir>`
### Delete with Kustomize: `kubectl delete -k <kustomization_dir>`

## Access running apps via port forwarding 

- `kubectl port-forward <resource_name> <local_port>:<port_in_k8s>` accesses port `<port_in_k8s>` in the resource in the cluster via `127.0.0.1:<local_port>`.
- `kubectl port-forward <resource_name> :<port_in_k8s>` accesses port `<port_in_k8s>` in the resource in the cluster via an automatically selected port in `127.0.0.1`.

`resource` can be either a pod, replicaset or service.

kubectl port-forward vehicle-quotes-deployment-6bd65d9b87-m5wqr 5000:5000

## Create service that exposes a deployment

`kubectl expose deployment <deployment_name> --type=NodePort --name=<service_name>`

kubectl expose deployment nginx-deployment --type=NodePort --name=nginx-service

### The service can be accessed by:

1. The `CLUSTER-IP` obtained from `kubectl get services`.
2. Using the `NodePort` obtained from `kubectl describe services <service_name>` and going to `127.0.0.1:<NodePort>`
3. Using the `INTERNAL-IP` obtained from `kubectl get nodes --output=wide` along with the `NodePort` obtained from `kubectl describe services <service_name>` and going to `<INTERNAL-IP>:<NodePort>`.
4. If the kube-dns (CoreDNS) is running, via its name and namespace with `<service_name>.<service_namespace>:port`
5. Via automatically generated environment variables in the pods that were created after the service existed. There are many. They contain the host and port. `printenv` to see them.

I think this all works because in a local microk8s installation, the host machine is a node (the only one) in the k8s cluster.

# Running containers with my own images

https://microk8s.io/docs/registry-images
https://microk8s.io/docs/registry-built-in

# working with microk8s built-in registry

- Build the image: `docker build . -f Dockerfile.dev -t localhost:32000/vehicle-quotes-dev:registry`.
- Push to k8s registry: `docker push localhost:32000/vehicle-quotes-dev:registry`.
- List and remove images: `microk8s ctr images --help`.

# How to define environment variables via config maps

https://kubernetes.io/docs/tasks/configure-pod-container/configure-pod-configmap/

# Storage
https://kubernetes.io/docs/tasks/configure-pod-container/configure-persistent-volume-storage/

# Init containers:
https://kubernetes.io/docs/tasks/configure-pod-container/configure-pod-initialization/

# kustomize
https://kubernetes.io/docs/tasks/manage-kubernetes-objects/kustomization/
https://github.com/kubernetes-sigs/kustomize
https://kubectl.docs.kubernetes.io/guides/introduction/kustomize/


# Gotchas on dockerizing and k8s'izing .NET apps:
- Make sure that the `.devcontainer/devcontainer.json` uses `0.0.0.0` instead of `localhost` in the `applicationUrl` field. So that the web server can be accessed from outside the container.

# Accesssing github
ssh-keygen -t ed25519 -C "megakevinx@gmail.com"
pw: `this is my ssh key`

# Connect to db in another pod via service from the app pod 

`psql -h vehicle-quotes-db-service -U vehicle_quotes`

# Connect to db in pod via service from the local machine

`psql -h localhost -p <node_port> -U vehicle_quote`

`psql -h <service_ip> -U vehicle_quote`

`service_ip` and `node_port` can be obtained from `kubectl describe services <service_name>`


kubectl scale deployment nginx-deployment --replicas=0
kubectl scale deployment nginx-deployment --replicas=2

kubectl exec nginx-deployment-559d658b74-hffwv -- printenv
kubectl exec nginx-deployment-559d658b74-gzgmk -- printenv



kubectl apply -f service.yaml && kubectl apply -f persistent-volume.yaml && kubectl apply -f persistent-volume-claim.yaml && kubectl apply -f deployment.yaml


kubectl delete -f deployment.yaml && kubectl delete -f persistent-volume-claim.yaml && kubectl delete -f persistent-volume.yaml && kubectl delete -f service.yaml

kubectl diff -f persistent-volume.yaml

wait for postgres: https://medium.com/@xcoulon/initializing-containers-in-order-with-kubernetes-18173b9cc222


