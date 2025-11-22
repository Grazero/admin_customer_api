#!/bin/bash
echo "Deploying to server..."
rsync -avz \
  --exclude='bin/' \
  --exclude='obj/' \
  --exclude='.vscode/' \
  --exclude='.git/' \
  --exclude='*.db' \
  --exclude='*.sqlite' \
  /Users/chaiyawit/Documents/GitHub/admin_customer_api/ \
  lek@58.11.9.214:/home/lek/admin_customer_api/

echo "Restarting container on server..."
ssh lek@58.11.9.214 "cd /home/lek/admin_customer_api && docker-compose down && docker system prune -f && docker-compose -f docker-compose.prod.yml up -d --build"

echo "Deployment completed!"