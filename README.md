
# GrapePhotoApp - Microservices Architecture(Cloud Native) Application (VS 2017 and AWS Server based)
Sample .NET Core application, based on a simplified microservices architecture. <p>
 
<img src="GrapePhoto/GithubImages/arthitech.png">


Grape Photo Web Application is hosted in EC2 (Windows Server 2016) as simulator of Mobile App to
allow users to post. All application requests are routed by API Gateway to Lambda Functions. Each
Lambda function is a micro-service to manipulate data of DynamoDB. Otherwise, the posted images can
be saved to S3 Bucket directly. When the image saved, the bucket triggers one Lambda Function to call
Rekognition API to analyse image and results saved to DynamoDB. Most importantly, those analysis
results of image can be visualized with 3rd Party Service Redash, so that business users are able to easily
make business strategy based on Redash Dashboard. While, anther 3rd Party service Pusher is used to send
notification to posters’ followers.

<h2>API Gateway and AWS Lambda</h2>

AWS Lambda and API Gateway services are used to build the Serverless REST API.
Amazon API Gateway is a fully managed service that provides the entire life cycle management of the
APIs, including: publish, maintain, monitor, and secure at any scale. It authenticates, manages, and
monitors API calls from external apps and passes them to AWS services like EC2, DynamoDB and AWS
Lambda.
AWS Lambda is a serverless computing platform that executes code as functions without provisioning or
managing servers. The AWS Lambda only executes when the requests are received, and the performance is
supported by the high-availability compute infrastructure. Management of underlying infrastructure is not
needed any more. Beside AWS Lambda provides various runtimes and supports different languages,
including Node.js and Python.
<h2> Database and Storage</h2>
Amazon DynamoDB is chosen as a fully managed proprietary NoSQL database service, which provides
fast and predictable performance with seamless scalability. In the project, all the transaction data, user
portfolios, posts information and content analysing data across the social media websites have been stored
in DynamoDB. Meanwhile, Amazon S3 is used to securely collect, store, and analyse the data at a massive
scale. S3 provides query-in-place functionality, allowing the machine learning services to run powerful
analytics directly on voluminous social media files storedat rest in S3.
 

 
<h2>Dashboard</h2>
Redash is used to connect and query AWS DynamoDB, build dashboards to visualize data and share them
with whom concerned to analyse the social media network. Dashboard is focusing on virtualizing the user
activities and posts content popularity, which can bring in tremendous market benefits by user tagging and
precise advertisement targeting.

<h2>CI/CD</h2>
GitHub is used as source control system where multiple developers can push and commit code. Jenkins, as
open source platform of CI/CD, is used for pulling source code from GitHub, restore, build and publish on
real time basis. The delivery process automatically restarts Internet Information Server. In this case, user
could commit code to GitHub and simply refresh the website.
